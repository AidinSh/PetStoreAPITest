using System;
using System.Net.Http;
using System.Threading.Tasks;
using PetstoreApiTest.Models;
using PetstoreApiTest.Clients;
using PetstoreApiTests.Utils;
using Reqnroll;
using PetStoreAPITest.Context;

namespace PetStoreAPITest.StepDefinitions
{
    [Binding]
    public class PetsStepDefinitions
    {
        private readonly PetApiClient _client;
        private PetContext _petContext;
        private Pet _pet;
        private HttpResponseMessage _response;

        public PetsStepDefinitions(PetContext petContext)
        {
            _client = new PetApiClient();
            _petContext = petContext;
        }

        [Given("I have a new pet")]
        public void GivenIHaveANewPet()
        {
            _pet = DataGenerator.GeneratePet();
        }

        [When("I create a new pet in the Store")]
        public async Task WhenICreateANewPetInTheStore()
        {
            _response = await _client.CreatePetAsync(_pet);
            if (_response.IsSuccessStatusCode)
                _petContext.CreatedPet = _pet;
        }

        [When("I retrieve the pet by ID")]
        public void WhenIRetrieveThePetByID()
        {
            throw new PendingStepException();
        }

        [Then("I verify the pet details")]
        public void ThenIVerifyThePetDetails()
        {
            throw new PendingStepException();
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
