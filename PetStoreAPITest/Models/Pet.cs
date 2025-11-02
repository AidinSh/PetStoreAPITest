using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreAPITest.Models
{
    public class Pet
    {
        public long Id { get; set; }
        public Category Category { get; set; } = new Category();
        public string Name { get; set; } = string.Empty;
        public List<string> PhotoUrls { get; set; } = new List<string>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public string Status { get; set; } = string.Empty; 

    }
}
