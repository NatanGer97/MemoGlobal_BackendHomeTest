using MemoGlobal_BackendHomeTest.Models.DTO;
using MemoGlobal_BackendHomeTest.Models.Entity;
using MemoGlobal_BackendHomeTest.Models.Response;
using MemoGlobal_BackendHomeTest.Models.Response.ExsternalApiResponses;
using MemoGlobal_BackendHomeTest.Repos;
using Newtonsoft.Json;
using System.Security.Principal;

namespace MemoGlobal_BackendHomeTest.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<UserService> logger;
        private readonly IUserRepo userRepo;


        public UserService(ILogger<UserService> logger, IUserRepo userRepo)
        {
            httpClient = new HttpClient();
            this.logger = logger;
            this.userRepo = userRepo;

        }

        private readonly string baseUrl = "https://reqres.in/api/users";

        public async Task<UserResponse> CreateUser(CreateUserRequest createUserRequest)
        {
            UserResponse badUserResponse = new UserResponse("error while creating user");
            var response = await httpClient.PostAsJsonAsync(baseUrl, createUserRequest);
            if (response.IsSuccessStatusCode)
            {
                User user = deserializeResponse<User>(await response.Content.ReadAsStringAsync())!;
                // save to db
                await addToDB(user);

                return new UserResponse(user);
            }

            return badUserResponse;

        }

        public async Task<UserResponse> DeleteUser(int id)
        {
            // first check if user exist
            UserResponse userResponse = await GetUserById(id);
            if (!userResponse.IsSuccess)
            {
                logger.LogError(userResponse.ResponseMessage);
                return userResponse;
            }

            string url = baseUrl + $"/{id}";
            HttpResponseMessage response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // TODO: delete from db 
                User user = userResponse.User!;
                user.Id = id;


                await DeleteUserFromDB(user);

                return userResponse;
            }

            else // in case user is only in the db
            {
                User? user = await userRepo.findUserById(id);
                if (user != null)
                {
                    await DeleteUserFromDB(user);
                    return new UserResponse(user);
                }
                return new UserResponse("error while deleting user");
            }
        }

        private async Task DeleteUserFromDB(User user)
        {
            // delete iff user in db
            User userFromDB = await userRepo.findUserById(user.Id);
            if (userFromDB != null)
            {
                logger.LogInformation("user in db:" + userFromDB);

                userRepo.Delete(user);
            }
            else { logger.LogError("user not in db"); }
        }

        public async Task<UsersResponse> ReadUserFromPage(int page)
        {
            string url = baseUrl + $"?page={page}";
            UsersResponse badResponse = new UsersResponse("bad response");
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                ListOfUserData listOfUserData = this.deserializeResponse<ListOfUserData>(await response.Content.ReadAsStringAsync())!;
                logger.LogInformation($"recived: {listOfUserData}");

                await addToDB(listOfUserData.usersList);

                return new UsersResponse(listOfUserData.usersList);
            }

            return badResponse;

        }

        private async Task addToDB(List<User> usersList)
        {
            await userRepo.AddUsers(usersList);
        }


        private async Task addToDB(User user)
        {
            await userRepo.AddUser(user);

        }

        public async Task<UserResponse> UpdateUser(int id, CreateUserRequest createUserRequest)
        {
            string url = baseUrl + $"/{id}";
            UserResponse userResponse = await GetUserById(id);

            if (!userResponse.IsSuccess)
            {
                logger.LogError(userResponse.ResponseMessage);

                return userResponse;
            }

            var response = await httpClient.PutAsJsonAsync<CreateUserRequest>(url, createUserRequest);

            if (response.IsSuccessStatusCode)
            {
                User user = this.deserializeResponse<User>(await response.Content.ReadAsStringAsync())!;
                User OldUser = userResponse.User!;

                user.Id = id;
                UpdateUserInDB(id, user);

                logger.LogInformation($"user updated: {user}");

                return new UserResponse(user);
            }
            else
            {
                string msg = "error while updating user";
                logger.LogError(msg);
                return new UserResponse(msg);
            }

        }

        private void UpdateUserInDB(int id, User user)
        {
            userRepo.Update(id, user);

        }

        public async Task<UserResponse> GetUserById(int userId)
        {
            string url = baseUrl + $"/{userId}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseAsString = await response.Content.ReadAsStringAsync();
                UserData userData = this.deserializeResponse<UserData>(responseAsString)!;
                logger.LogInformation($"User found: {userData.user}");

                await addToDB(userData.user);

                return new UserResponse(userData.user);
            }
            else
            {
                logger.LogError("user not found");
                return new UserResponse("user not found");
            }

        }

        private T? deserializeResponse<T>(string responseAsString)
        {
            T? responseAsObject = JsonConvert.DeserializeObject<T>(responseAsString);

            return responseAsObject;

        }

        private BasicResponse buildResponse(object obj)
        {
            Type type = obj.GetType();


            if (type.Equals(typeof(User)))
            {
                return new UserResponse((User)obj);
            }

            else if (type.Equals(typeof(string)))
            {
                return new UserResponse((string)obj);
            }

            else if (type.Equals(typeof(List<User>)))
            {
                return new UsersResponse((List<User>)obj);
            }

            return null;



        }


    }
}
