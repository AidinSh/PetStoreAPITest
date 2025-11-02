using System;
using System.Collections.Generic;
using PetStoreAPITest.Models;
using PetstoreApiTest.Models;

namespace PetstoreApiTests.Utils
{
    public static class DataGenerator
    {
        private static readonly Random _random = new();

        public static Pet GenerateRandomPet(PetStatus status = PetStatus.Available)
        {
            return new Pet
            {
                Id = DateTime.UtcNow.Ticks,
                Name = $"Pet_{_random.Next(1000, 9999)}",
                Category = new Category
                {
                    Id = 1,
                    Name = "dog"
                },
                PhotoUrls = new List<string> { "https://example.com/photo.jpg" },
                Tags = new List<Tag>
                {
                    new Tag { Id = 1, Name = "cute" }
                },
                Status = status
            };
        }
    }
}
