using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using PetstoreApiTest.Clients;
using PetstoreApiTest.Models;
using PetStoreAPITest.Utils;
using PetstoreApiTests.Utils;
using Reqnroll;

namespace PetStoreAPITest.StepDefinitions
{
    [Binding]
    public class PetsStepDefinitions
    {
        private readonly PetApiClient _client;
        private ScenarioContext _scenarioContext;
        private Pet? _pet;
        private HttpResponseMessage? _response;
        private SchemaGenerator _schemaGenerator;

        public PetsStepDefinitions(ScenarioContext scenarioContext)
        {
            _client = new PetApiClient();
            _scenarioContext = scenarioContext;
            _schemaGenerator = new SchemaGenerator();
        }

        [Given("Have a new Pet")]
        public void HaveANewPet()
        {
            _pet = DataGenerator.GeneratePet();
        }

        [Given("Have an existing pet")]
        public async Task HaveAnExistingPet()
        {
            _pet = DataGenerator.GeneratePet();
            await CreateANewPet(_pet);
        }

        [Given("Have at least one (.*) pet")]
        public async Task HaveAtLeastOnePetWithStatus(PetStatus status)
        {
            _pet = DataGenerator.GeneratePet(status);
            var response = await _client.CreatePetAsync(_pet);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _scenarioContext["CreatedPet"] = _pet;
        }


        [When("Update the pet name")]
        public async Task UpdateThePetName()
        {
            var petToUpdate = (Pet)_scenarioContext["CreatedPet"];
            petToUpdate.Name = "Updated_Pet_Name";
            _scenarioContext["UpdatedPet"] = petToUpdate;
            var response = await _client.UpdatePetAsync(petToUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await Task.Delay(5000); //Wait for the update to get place
        }

        [When("Create a new pet in the store")]
        public async Task CreateANewPetInTheStore()
        {
            _response = await CreateANewPet(_pet!);
        }

        [When("Retrieve the pet by ID")]
        public async Task RetrieveThePetByID()
        {
            _response = await _client.GetPetByIDWithRetryAsync(_pet!.Id, 5);
            //getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            if (_response.StatusCode == HttpStatusCode.OK)
                _scenarioContext["RetrivedPet"] = JsonConvert.DeserializeObject<Pet>(await _response.Content.ReadAsStringAsync())!;
        }

        [When("Search for pets by status (.*)")]
        public async Task SearchForPetsByStatus(PetStatus status)
        {
            _response = await _client.FindPetsByStatusAsync(status);
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [When("Delete Pet by ID")]
        public async Task DeletePetByID()
        {
            _response = await _client.DeletePetWithRetryAsync(_pet!.Id, 5); //Deleting pet with retry to avoid flakiness
        }


        [Then("Verify Pet details")]
        public void VerifyThePetDetails()
        {
            _response!.StatusCode.Should().Be(HttpStatusCode.OK); //Checks status code 
            var createdPet = (Pet)_scenarioContext["CreatedPet"]; 
            var retrivedPet = (Pet)_scenarioContext["RetrivedPet"];
            retrivedPet.Name.Should().Be(createdPet.Name); //checking all fields of Pet
            retrivedPet.Id.Should().Be(createdPet.Id);
            retrivedPet.Status.Should().Be(createdPet.Status);
            retrivedPet.Category.Name.Should().Be(createdPet.Category.Name);
            retrivedPet.Category.Id.Should().Be(createdPet.Category.Id);
            retrivedPet.PhotoUrls.Should().BeEquivalentTo(createdPet.PhotoUrls);
            retrivedPet.Tags.Should().BeEquivalentTo(createdPet.Tags);
        }

        [Then("Veirfy updates on pet details")]
        public void VeirfyUpdatesOnPetDetails()
        {
            var retrivedPet = (Pet)_scenarioContext["RetrivedPet"];
            var updatedPet = (Pet)_scenarioContext["UpdatedPet"];
            _pet!.Name.Should().Be(retrivedPet.Name);
        }

        [Then("Verify search results contain only pets with status (.*)")]
        public async Task VerifySearchResultsContainOnlyPetsWithStatus(PetStatus status)
        {
            var listOfPets = JsonConvert.DeserializeObject<List<Pet>>(await (_response!).Content.ReadAsStringAsync());
            foreach (var pet in listOfPets!)
            {
                pet.Status.Should().Be(status);
            }
        }

        [Then("Validate the response schema for search results")]
        public async Task ValidateResponseSchemaForSearchResults()
        {
            JArray jsonResponse = JArray.Parse(await _response!.Content.ReadAsStringAsync());
            var isValid = jsonResponse.IsValid(_schemaGenerator.FindPetByStatusSchema(), out IList<string> errorMessages);
            Assert.That(isValid, Is.True, "Schema validation failed: " + string.Join("\n ", errorMessages));
        }

        [Then("Verify the pet is deleted successfully")]
        public async Task VerifyThePetIsDeletedSuccessfully()
        {
            var getResponse = await _client.GetPetByIdAsync(_pet!.Id);
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Given("Have a non-existing pet ID")]
        public async Task HaveANon_ExistingPetID()
        {
            _pet = DataGenerator.GeneratePet();
            _pet.Id = 4321;
            await _client.DeletePetWithRetryAsync(_pet.Id, 5);
        }

        [Then("Verify the response indicates pet not found")]
        public void VerifyTheResponseIndicatesPetNotFound()
        {
            _response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        private async Task<HttpResponseMessage> CreateANewPet(Pet pet)
        {
            var response = await _client.CreatePetAsync(pet);
            if (response.IsSuccessStatusCode)
            {
                _scenarioContext["CreatedPet"] = pet;
                Console.WriteLine($"Generated Pet ID: {_pet!.Id}, Name: {_pet.Name}");
            }     
            return response;
        }
    }
}
