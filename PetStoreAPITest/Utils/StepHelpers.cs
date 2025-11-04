using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetstoreApiTest.Models;
using Reqnroll;

namespace PetStoreAPITest.Utils
{
    public class StepHelpers
    {
        // Transforms a string status to the corresponding PetStatus enum
        [StepArgumentTransformation]
        public PetStatus TransformStatus(string status)
        {
            return status.ToUpper() switch
            {
                "AVAILABLE" => PetStatus.AVAILABLE,
                "PENDING" => PetStatus.PENDING,
                "SOLD" => PetStatus.SOLD,
                _ => throw new ArgumentException("Invalid status")
            };
        }
    }
}
