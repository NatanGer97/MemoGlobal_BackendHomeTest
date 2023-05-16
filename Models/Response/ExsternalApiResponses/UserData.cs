using MemoGlobal_BackendHomeTest.Models.Entity;
using Newtonsoft.Json;

namespace MemoGlobal_BackendHomeTest.Models.Response.ExsternalApiResponses
{
    public class UserData
    {
        [JsonProperty("data")]
        public User Data { get; set; }
    }
}
