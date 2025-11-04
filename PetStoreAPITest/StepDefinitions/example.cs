/*using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using PetstoreApiTest.Models;
using PetstoreApiTest.Clients;
using PetstoreApiTests.Utils;
using Reqnroll;

namespace PetstoreApiTests.StepDefinitions
{
    [Binding]
    public class PetSteps
    {
        private readonly PetApiClient _client;
        private ScenarioContext _scenarioContext;
        private Pet _pet;
        private HttpResponseMessage _response;

        public PetSteps(ScenarioContext scenarioContext)
        {
            _client = new PetApiClient();
            _scenarioContext = scenarioContext;
        }
        

        [Given(@"I have a new pet")]
        public void GivenIHaveANewPet()
        {
            _pet = DataGenerator.GeneratePet();
        }

        [Given(@"I have an existing pet")]
        public async Task GivenIHaveAnExistingPet()
        {
            _pet = DataGenerator.GeneratePet();
            _response = await _client.CreatePetAsync(_pet);
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [When(@"I create the pet")]
        public async Task WhenICreateThePet()
        {
            _response = await _client.CreatePetAsync(_pet);
        }

        [Then(@"the pet should be created successfully")]
        public async Task ThenThePetShouldBeCreatedSuccessfully()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await _response.Content.ReadAsStringAsync();
            var responsePet = JsonConvert.DeserializeObject<Pet>(content);
            responsePet.Name.Should().Be(_pet.Name);
        }

        [Then(@"I can retrieve it by ID")]
        public async Task ThenICanRetrieveItById()
        {
            var getResponse = await _client.GetPetByIdAsync(_pet.Id);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var petData = JsonConvert.DeserializeObject<Pet>(await getResponse.Content.ReadAsStringAsync());
            petData.Id.Should().Be(_pet.Id);
        }

        [When(@"I update the pet details")]
        public async Task WhenIUpdateThePetDetails()
        {
            _pet.Name = "Updated-" + _pet.Name;
            _response = await _client.UpdatePetAsync(_pet);
        }

        [Then(@"the pet details should be updated")]
        public async Task ThenThePetDetailsShouldBeUpdated()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getResponse = await _client.GetPetByIdAsync(_pet.Id);
            var updatedPet = JsonConvert.DeserializeObject<Pet>(await getResponse.Content.ReadAsStringAsync());
            updatedPet.Name.Should().Be(_pet.Name);
        }

        [When(@"I delete the pet")]
        public async Task WhenIDeleteThePet()
        {
            _response = await _client.DeletePetAsync(_pet.Id);
        }

        [Then(@"the pet should not be retrievable")]
        public async Task ThenThePetShouldNotBeRetrievable()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getResponse = await _client.GetPetByIdAsync(_pet.Id);
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
*/