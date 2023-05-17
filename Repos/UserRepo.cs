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
            await Context.SaveChangesAsync();
        }


        public async Task AddUsers(List<User> users)
        {
            await Context.Users.AddRangeAsync(users);
            await Context.SaveChangesAsync();
        }

        public void Delete(User user)
        {
            Context.Users.Remove(user);
            Context.SaveChanges();


        }

        public async Task<User?> findUserById(int id)
        {
            return await Context.Users.FindAsync(id);
        }

        public void Update(int id, User newUser)
        {
            Context.Users.Update(newUser);
            Context.SaveChanges();

           /* User? user = Context.Users.Find(id);
            if (user != null)
            {
                Context.Entry(user).CurrentValues.SetValues(newUser);
                Context.SaveChanges();



            }*/


            
        }
    }
}
