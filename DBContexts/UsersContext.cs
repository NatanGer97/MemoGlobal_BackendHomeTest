using MemoGlobal_BackendHomeTest.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace MemoGlobal_BackendHomeTest.DBContexts
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().Property(user => user.Email).IsRequired()
                .HasColumnName("email");
            modelBuilder.Entity<User>().Property(user => user.Id)
                .HasColumnName("id");
            modelBuilder.Entity<User>().Property(user => user.Avatar).IsRequired()
                .HasColumnName("avatar");
            modelBuilder.Entity<User>().Property(user => user.FirstName).IsRequired()
                .HasColumnName("first_name");
            modelBuilder.Entity<User>().Property(user => user.LastName).IsRequired()
                .HasColumnName("last_name");

            
        }
    }
}
