using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MemoGlobal_BackendHomeTest.Models.DTO
{
    public class CreateUserRequest
    {
        public string  Email { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        public string Avatar { get; set; }

        
    }
}
