using MemoGlobal_BackendHomeTest.Models.DTO;
using MemoGlobal_BackendHomeTest.Models.Entity;
using MemoGlobal_BackendHomeTest.Models.Response;
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

        public async Task<UserResponse> CreateUser(CreateUserRequest createUserRequest)
        {
            UserResponse badUserResponse = new UserResponse("error while creating user");
            var response = await httpClient.PostAsJsonAsync(baseUrl, createUserRequest);
            if (response.IsSuccessStatusCode)
            {
                string responseAsString = await response.Content.ReadAsStringAsync();

                User? user = JsonConvert.DeserializeObject<User>(responseAsString);

                if (user != null)
                {
                    return new UserResponse(user);
                }
                return badUserResponse;

                // TODO: save to db               
            }

            return badUserResponse;
        }

        public async Task<UserResponse> DeleteUser(int id)
        {
            // first check if user exist
            UserResponse userResponse = await GetUserById(id);
            if (!userResponse.IsSuccess)
            {
                return userResponse;
            }

            string url = baseUrl + $"/{id}";
            HttpResponseMessage response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return userResponse;
            }

            else
            {
                return new UserResponse("error while deleting user");
            }



        }


        public async Task<UsersResponse> ReadUserFromPage(int page)
        {
            string url = baseUrl + $"?page={page}";
            UsersResponse badResponse = new UsersResponse("bad response");
            var response = await httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                string responseAsString = await response.Content.ReadAsStringAsync();
                ListOfUserData? listOfUserData = JsonConvert.DeserializeObject<ListOfUserData>(responseAsString);
                if (listOfUserData != null)
                {
                    return new UsersResponse(listOfUserData.usersList);
                }
                

                return badResponse;

            }

            return badResponse;
        }

        public async Task<UserResponse> UpdateUser(int id, CreateUserRequest createUserRequest)
        {
            string url = baseUrl + $"/{id}";
            UserResponse userResponse = await GetUserById(id);
            if (!userResponse.IsSuccess)
            {
                return userResponse;
            }

            var response = await httpClient.PutAsJsonAsync<CreateUserRequest>(url, createUserRequest);


            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                User? user = JsonConvert.DeserializeObject<User>(responseAsString);

                if (user != null)
                {
                    return new UserResponse(user);
                }

                return new UserResponse("updating user gone wrong");
            }

            return new UserResponse("updating user gone wrong");
        }

        public async Task<UserResponse> GetUserById(int userId)
        {
            string url = baseUrl + $"/{userId}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseAsString = await response.Content.ReadAsStringAsync();
                UserData? userData = JsonConvert.DeserializeObject<UserData>(responseAsString);

                if (userData != null)
                {
                    return new UserResponse(userData.user);
                }

                return new UserResponse(new User());

            }
            else
            {
                return new UserResponse("user not found");
            }




        }
    }
}
