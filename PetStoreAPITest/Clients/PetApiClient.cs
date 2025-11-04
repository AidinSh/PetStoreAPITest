using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetStoreAPITest.Utils;
using PetstoreApiTest.Models;
using System.Net;


namespace PetstoreApiTest.Clients
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

            //It is not neccessary to authenticate for using these APIs but it is just for demonstration
            if (!string.IsNullOrEmpty(ConfigManager.ApiKey))
                _client.DefaultRequestHeaders.Add("api_key", ConfigManager.ApiKey);

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

        //This method is used because the service is unstable and retring helps to get rid of flakiness
        public async Task<HttpResponseMessage> GetPetByIDWithRetryAsync(long id, int maxRetries)
        {
            var response = await GetPetByIdAsync(id);
            for (int i=0; i< maxRetries; i++)
            {
                if (response.StatusCode.Equals(HttpStatusCode.OK)) {
                    return response;
                }
                await Task.Delay(1000);
                response = await GetPetByIdAsync(id);
            }
            return response;
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

        //Same as GetPet API this service is also unstable
        public async Task<HttpResponseMessage> DeletePetWithRetryAsync(long id, int maxRetries)
        {
            var response = await DeletePetAsync(id);
            for (int i = 0; i < maxRetries; i++)
            {  
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    return response;
                }
                await Task.Delay(1000);
                response = await DeletePetAsync(id);
            }
            return response;
        }

        public async Task<HttpResponseMessage> FindPetsByStatusAsync(PetStatus status)
        {
            return await _client.GetAsync($"pet/findByStatus?status={status.ToString().ToLower()}");
        }
    }
}
