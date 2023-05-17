using MemoGlobal_BackendHomeTest.Models.DTO;
using MemoGlobal_BackendHomeTest.Models.Entity;
using MemoGlobal_BackendHomeTest.Models.Response;

namespace MemoGlobal_BackendHomeTest.Services
{
    public interface IUserService
    {
        public Task<UsersResponse> ReadUserFromPage(int page);
        public Task<UserResponse> CreateUser(CreateUserRequest createUserRequest);

        public Task<UserResponse> DeleteUser(int id);
        public Task<UserResponse> UpdateUser(int id, CreateUserRequest createUserRequest);

        public Task<UserResponse> GetUserById(int userId);
    }
}
