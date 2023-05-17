using MemoGlobal_BackendHomeTest.Models.Entity;

namespace MemoGlobal_BackendHomeTest.Repos
{
    public interface IUserRepo
    {
        Task AddUser(User user);
        Task AddUsers(List<User> users);
        void Update(User newUser, User oldUser);
        void Delete(User user);
    }
}
