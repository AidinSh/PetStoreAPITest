using System;
using System.Collections.Generic;
using System.IO;
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
        private Pet _pet;
        private HttpResponseMessage _response;
        private SchemaGenerator _schemaGenerator;

        public PetsStepDefinitions(ScenarioContext scenarioContext)
        {
            _client = new PetApiClient();
            _scenarioContext = scenarioContext;
            _schemaGenerator = new SchemaGenerator();
        }

        [Given("I have a new pet")]
        public void GivenIHaveANewPet()
        {
            _pet = DataGenerator.GeneratePet();
            Console.WriteLine($"Generated Pet ID: {_pet.Id}, Name: {_pet.Name}");
        }

        [When("I create a new pet in the Store")]
        public async Task CreateANewPetInTheStore()
        {
            _response = await CreateANewPet(_pet);
        }

        [When("I retrieve the pet by ID")]
        public async Task WhenIRetrieveThePetByID()
        {
            var getResponse = await _client.GetPetByIDWithRetryAsync(_pet.Id, 5);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            _pet = JsonConvert.DeserializeObject<Pet>(await getResponse.Content.ReadAsStringAsync())!;
        }

        [Then("I verify the pet details")]
        public void ThenIVerifyThePetDetails()
        {
            var createdPet = (Pet)_scenarioContext["CreatedPet"];
            _pet.Name.Should().Be(createdPet.Name);
            _pet.Id.Should().Be(createdPet.Id);
            _pet.Status.Should().Be(createdPet.Status);
            _pet.Category.Name.Should().Be(createdPet.Category.Name);
            _pet.Category.Id.Should().Be(createdPet.Category.Id);
            _pet.PhotoUrls.Should().BeEquivalentTo(createdPet.PhotoUrls);
            _pet.Tags.Should().BeEquivalentTo(createdPet.Tags);
        }

        [Given("I have an existing pet")]
        public async Task GivenIHaveAnExistingPet()
        {
            _pet = DataGenerator.GeneratePet();
            await CreateANewPet(_pet);
        }

        [When("I update the pet name")]
        public async Task WhenIUpdateThePetDetails()
        {
            _pet.Name = "Updated_Pet_Name";
            var response = await _client.UpdatePetAsync(_pet);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _scenarioContext["CreatedPet"] = _pet;
        }

        [Given("I have at least one (.*) pet")]
        public async Task GivenIHaveAtLeastOnePet(PetStatus status)
        {
            _pet = DataGenerator.GeneratePet(status);
            var response = await _client.CreatePetAsync(_pet);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [When("I search for pets by status (.*)")]
        public async Task WhenISearchForPetsByStatus(PetStatus status)
        {
            _response = await _client.FindPetsByStatusAsync(status);
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then("I verify the search results contain only pets with status (.*)")]
        public async Task ThenIVerifyTheSearchResultsContainOnlyPetsWithStatus(PetStatus status)
        {
            var listOfPets = JsonConvert.DeserializeObject<List<Pet>>(await(_response).Content.ReadAsStringAsync());
            foreach (var pet in listOfPets!)
            {
                pet.Status.Should().Be(status);
            }
        }

        [Then("I validate the response schema for the search results")]
        public async Task ThenIValidateTheResponseSchemaForTheSearchResults()
        {
            JArray jsonResponse = JArray.Parse(await _response.Content.ReadAsStringAsync());
            var isValid = jsonResponse.IsValid(_schemaGenerator.FindPetByStatusSchema(), out IList<string> errorMessages);
            Assert.That(isValid, Is.True, "Schema validation failed: " + string.Join("\n ", errorMessages));        
        }

        [When("I delete the pet by ID")]
        public async Task WhenIDeleteThePetByID()
        {
            await _client.DeletePetWithRetryAsync(_pet.Id, 5);
        }

        [Then("I verify the pet is deleted successfully")]
        public async Task ThenIVerifyThePetIsDeletedSuccessfully()
        {
            var getResponse = await _client.GetPetByIdAsync(_pet.Id);
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private async Task<HttpResponseMessage> CreateANewPet(Pet pet)
        {
            var response = await _client.CreatePetAsync(pet);
            if (response.IsSuccessStatusCode)
                _scenarioContext["CreatedPet"] = pet;
            return response;
        }
    }
}
