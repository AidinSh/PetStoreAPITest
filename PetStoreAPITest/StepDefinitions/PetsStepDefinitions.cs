using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using PetstoreApiTest.Clients;
using PetstoreApiTest.Models;
using PetStoreAPITest.Context;
using PetstoreApiTests.Utils;
using Reqnroll;

namespace PetStoreAPITest.StepDefinitions
{
    [Binding]
    public class PetsStepDefinitions
    {
        private readonly PetApiClient _client;
        private ScenarioContext _scenarioContext;
        private Pet ?_pet;
        private HttpResponseMessage _response;

        public PetsStepDefinitions(ScenarioContext scenarioContext)
        {
            _client = new PetApiClient();
            _scenarioContext = scenarioContext;
        }

        [Given("I have a new pet")]
        public void GivenIHaveANewPet()
        {
            _pet = DataGenerator.GeneratePet();
            Console.WriteLine($"Generated Pet ID: {_pet.Id}, Name: {_pet.Name}");
        }

        [When("I create a new pet in the Store")]
        public async Task WhenICreateANewPetInTheStore()
        {
            _response = await _client.CreatePetAsync(_pet);
            if (_response.IsSuccessStatusCode)
                _scenarioContext["CreatedPet"] = _pet;
        }

        [When("I retrieve the pet by ID")]
        public async Task WhenIRetrieveThePetByID()
        {
            var getResponse = await _client.GetPetByIDWithRetryAsync(_pet.Id, 5);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            _pet = JsonConvert.DeserializeObject<Pet>(await getResponse.Content.ReadAsStringAsync());
        }

        [Then("I verify the pet details")]
        public void ThenIVerifyThePetDetails()
        {
            var createdPet = (Pet)_scenarioContext["CreatedPet"];
            _pet.Name.Should().Be(createdPet.Name, "because pet name should be the same");
            _pet.Id.Should().Be(createdPet.Id, "because pet Id should be the same");
            _pet.Status.Should().Be(createdPet.Status, "because pet status should be the same");
            _pet.Category.Name.Should().Be(createdPet.Category.Name, "because pet category should be the same");
            _pet.Category.Id.Should().Be(createdPet.Category.Id, "because pet category Id should be the same");
            _pet.PhotoUrls.Should().BeEquivalentTo(createdPet.PhotoUrls, "because photo URLs should be the same");
            _pet.Tags.Should().BeEquivalentTo(createdPet.Tags, "because tags should be the same");
        }

        [Given("I have an existing pet")]
        public void GivenIHaveAnExistingPet()
        {
            throw new PendingStepException();
        }

        [When("I update the pet details")]
        public void WhenIUpdateThePetDetails()
        {
            throw new PendingStepException();
        }

        [Given("I have pets with various statuses")]
        public void GivenIHavePetsWithVariousStatuses()
        {
            throw new PendingStepException();
        }

        [When("I search for pets by status {string}")]
        public void WhenISearchForPetsByStatus(string available)
        {
            throw new PendingStepException();
        }

        [Then("I verify the search results contain only pets with status {string}")]
        public void ThenIVerifyTheSearchResultsContainOnlyPetsWithStatus(string available)
        {
            throw new PendingStepException();
        }

        [Then("I validate the response schema for the search results")]
        public void ThenIValidateTheResponseSchemaForTheSearchResults()
        {
            throw new PendingStepException();
        }

        [When("I delete the pet by ID")]
        public void WhenIDeleteThePetByID()
        {
            throw new PendingStepException();
        }

        [Then("I verify the pet is deleted successfully")]
        public void ThenIVerifyThePetIsDeletedSuccessfully()
        {
            throw new PendingStepException();
        }
    }
}
