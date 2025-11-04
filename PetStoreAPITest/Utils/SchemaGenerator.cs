using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace PetStoreAPITest.Utils
{
    //Generates JSON Schemas for validating API responses
    public class SchemaGenerator
    {
        public JSchema FindPetByStatusSchema()
        {
            var stringSchema = File.ReadAllText("jsonSchemas\\find_pet_by_status_schema.json");
            return JSchema.Parse(stringSchema);
        }

        public JSchema GetPetByIDSchema()
        {
            var stringSchema = File.ReadAllText("jsonSchemas\\get_pet_schema.json");
            return JSchema.Parse(stringSchema);
        }


    }
}
