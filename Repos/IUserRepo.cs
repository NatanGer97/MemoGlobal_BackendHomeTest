using MemoGlobal_BackendHomeTest.Models.Entity;

namespace MemoGlobal_BackendHomeTest.Repos
{
    public interface IUserRepo
    {
        Task AddUser(User user);
        Task AddUsers(List<User> users);
        void Update(int id, User newUser);
        void Delete(User user);
        Task<User> findUserById(int id);
    }
}
