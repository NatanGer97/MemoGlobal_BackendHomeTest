using MemoGlobal_BackendHomeTest.Models.DTO;
using MemoGlobal_BackendHomeTest.Models.Entity;
using MemoGlobal_BackendHomeTest.Models.Response.ExsternalApiResponses;
using Newtonsoft.Json;

namespace MemoGlobal_BackendHomeTest.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService()
        {
            httpClient = new HttpClient();
        }
        
        private readonly string baseUrl = "https://reqres.in/api/users";

        public Task<User> CreateUser(CreateUserRequest createUserRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> ReadUser(int id)
        {
            string url = baseUrl + $"/{id}";
            string response = await httpClient.GetStringAsync(url);
            
            UserData? userData = JsonConvert.DeserializeObject<UserData>(response);

            return userData.user;

        }

        public async Task<List<User>> ReadUserFromPage(int page)
        {
            string url = baseUrl + $"?page={page}";

            string response = await httpClient.GetStringAsync(url);
            ListOfUserData? listOfUserData = JsonConvert.DeserializeObject<ListOfUserData>(response);

            return listOfUserData.usersList;


        }

        public Task UpdateUser(int id, CreateUserRequest createUserRequest)
        {
            throw new NotImplementedException();
        }
    }
}
