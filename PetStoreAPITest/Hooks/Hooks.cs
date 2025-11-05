using System.Threading.Tasks;
using PetstoreApiTest.Clients;
using PetstoreApiTest.Models;
using Reqnroll;

namespace PetStoreAPITest.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly PetApiClient _client = new();
        ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        // This hook runs after scenarios to clean up created pets
        [AfterScenario]
        public async Task Cleanup()
        {
            if (_scenarioContext.ContainsKey("CreatedPet") && _scenarioContext["CreatedPet"] != null)
            {
                Pet petToDelete = (Pet)_scenarioContext["CreatedPet"];
                await _client.DeletePetWithRetryAsync(petToDelete.Id, 5);
            }
        }
    }
}
