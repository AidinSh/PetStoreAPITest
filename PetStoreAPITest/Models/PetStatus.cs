using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PetstoreApiTest.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PetStatus
    {
        [EnumMember(Value = "available")]
        AVAILABLE,

        [EnumMember(Value = "pending")]
        PENDING,

        [EnumMember(Value = "sold")]
        SOLD
    }
}
