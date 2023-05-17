using MemoGlobal_BackendHomeTest.DBContexts;
using MemoGlobal_BackendHomeTest.Models.Entity;

namespace MemoGlobal_BackendHomeTest.Repos
{
    public class UserRepo : BasicRepository, IUserRepo
    {
        private readonly ILogger<UserRepo> logger;


        
        public UserRepo(UsersContext context, ILogger<UserRepo> logger) 
            : base(context) 
        {
            this.logger = logger;
        }
      
        
        public async Task AddUser(User user)
        {

            try
            {
                User? userFromDb = Context.Users.Find(user.Id);
                if (userFromDb != null)
                {
                    await Context.Users.AddAsync(user);
                    await Context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }


        public async Task AddUsers(List<User> users)
        {
            try
            {

                await Context.Users.AddRangeAsync(users);
                await Context.SaveChangesAsync();
            } 
            catch(Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public void Delete(User user)
        {
            try
            {
                Context.Users.Remove(user);
                Context.SaveChanges();
            } 
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }


        }

        public async Task<User?> findUserById(int id)
        {
            return await Context.Users.FindAsync(id);
        }

        public void Update(int id, User newUser)
        {
            try
            {
                Context.Users.Update(newUser);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
            }

           /* User? user = Context.Users.Find(id);
            if (user != null)
            {
                Context.Entry(user).CurrentValues.SetValues(newUser);
                Context.SaveChanges();



            }*/


            
        }
    }
}
