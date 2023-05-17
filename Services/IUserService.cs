using MemoGlobal_BackendHomeTest.Models.DTO;
using MemoGlobal_BackendHomeTest.Models.Entity;

namespace MemoGlobal_BackendHomeTest.Services
{
    public interface IUserService
    {
        public Task<List<User>?> ReadUserFromPage(int page);
        public Task<User?> ReadUser(int id);
        public Task<User?> CreateUser(CreateUserRequest createUserRequest);

        public Task DeleteUser(int id);
        public Task UpdateUser(int id, CreateUserRequest createUserRequest);
    }
}
