using System.Collections.Generic;
using Newtonsoft.Json;
using PetStoreAPITest.Models;

namespace PetstoreApiTest.Models
{
    public class Pet
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; } = new();

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("photoUrls")]
        public List<string> PhotoUrls { get; set; } = new();

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; } = new();

        [JsonProperty("status")]
        public PetStatus Status { get; set; } = PetStatus.AVAILABLE;
    }
}
