using MemoGlobal_BackendHomeTest.Models.Entity;
using Newtonsoft.Json;

namespace MemoGlobal_BackendHomeTest.Models.Response.ExsternalApiResponses
{
    public class ListOfUserData
    {
        [JsonProperty("data")]
        public List<User> usersList{ get; set; }
    }
}
