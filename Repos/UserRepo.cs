using MemoGlobal_BackendHomeTest.DBContexts;
using MemoGlobal_BackendHomeTest.Models.Entity;

namespace MemoGlobal_BackendHomeTest.Repos
{
    public class UserRepo : BasicRepository, IUserRepo
    {

        public UserRepo(UsersContext context) 
            : base(context) { }
      
        
        public async Task AddUser(User user)
        {
            await Context.Users.AddAsync(user);
        }

        public async Task AddUsers(List<User> users)
        {
            await Context.Users.AddRangeAsync(users);
        }

        public void Delete(User user)
        {
            Context.Users.Remove(user);
        }

        public void Update(User newUser, User oldUser)
        {
            Context.Entry(oldUser).CurrentValues.SetValues(newUser);
        }
    }
}
