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

        public async Task<User?> CreateUser(CreateUserRequest createUserRequest)
        {
            User? responseUser = null;
            var response = await httpClient.PostAsJsonAsync(baseUrl, createUserRequest);
            if (response.IsSuccessStatusCode)
            {
                string responseAsString = await response.Content.ReadAsStringAsync();
                
                User? user = JsonConvert.DeserializeObject<User>(responseAsString);
                
                if (user != null)
                {
                    responseUser = user;
                }
                
                // TODO: save to db               
            }

            return responseUser;
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> ReadUser(int id)
        {
            string url = baseUrl + $"/{id}";
            var response = await httpClient.GetAsync(url);
            User? user = null;

            if (response.IsSuccessStatusCode)
            {
                string responseAsString = await response.Content.ReadAsStringAsync();
                UserData? userData = JsonConvert.DeserializeObject<UserData>(responseAsString);

                if (userData != null)
                {
                    user = userData.user;
                }

            }

            return user;
        }

        public async Task<List<User>?> ReadUserFromPage(int page)
        {
            string url = baseUrl + $"?page={page}";
            List<User>? results = null;

            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseAsString = await response.Content.ReadAsStringAsync();
                ListOfUserData? listOfUserData = JsonConvert.DeserializeObject<ListOfUserData>(responseAsString);
                 if (listOfUserData != null)
                {
                    results = listOfUserData.usersList;
                }
            }

            return results;
        }

        public Task UpdateUser(int id, CreateUserRequest createUserRequest)
        {
            throw new NotImplementedException();
        }
    }
}
