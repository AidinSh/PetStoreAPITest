using System;
using System.Collections.Generic;
using System.Xml.Linq;
using PetstoreApiTest.Models;
using PetStoreAPITest.Models;

namespace PetstoreApiTests.Utils
{
    public static class DataGenerator
    {
        private static readonly Random _random = new();

        //Generates a random Pet , but you can also create a pet with desired specifications by passing parameters
        public static Pet GeneratePet(    
            PetStatus status = PetStatus.AVAILABLE,
            string ?name = null,
            Category ?category = null,
            List<Tag> ?tags = null
            )
        {
            return new Pet
            {
                Id = DateTime.UtcNow.Ticks/100,
                Name = name ?? $"New_Pet{_random.Next(1000, 9999)}",
                Category = category ?? new Category
                {
                    Id = 1,
                    Name = "dog"
                },
                PhotoUrls = new List<string> { "https://example.com/photo.jpg" },
                Tags = tags ?? new List<Tag>
                {
                    new Tag { Id = 1, Name = "cute" }
                },
                Status = status
            };

        }

    }
}
