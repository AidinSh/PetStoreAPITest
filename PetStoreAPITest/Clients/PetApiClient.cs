using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetStoreAPITest.Utils;
using PetstoreApiTest.Models;


namespace PetstoreApiTests.Clients
{
    public class PetApiClient
    {
        private readonly HttpClient _client;

        public PetApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(ConfigManager.BaseUrl)
            };
        }

        public async Task<HttpResponseMessage> CreatePetAsync(Pet pet)
        {
            var content = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");
            return await _client.PostAsync("pet", content);
        }

        public async Task<HttpResponseMessage> GetPetByIdAsync(long id)
        {
            return await _client.GetAsync($"pet/{id}");
        }

        public async Task<HttpResponseMessage> UpdatePetAsync(Pet pet)
        {
            var content = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");
            return await _client.PutAsync("pet", content);
        }

        public async Task<HttpResponseMessage> DeletePetAsync(long id)
        {
            return await _client.DeleteAsync($"pet/{id}");
        }

        public async Task<HttpResponseMessage> FindPetsByStatusAsync(PetStatus status)
        {
            return await _client.GetAsync($"pet/findByStatus?status={status.ToString().ToLower()}");
        }
    }
}
