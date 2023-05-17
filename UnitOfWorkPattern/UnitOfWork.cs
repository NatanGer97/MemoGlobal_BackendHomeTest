using MemoGlobal_BackendHomeTest.DBContexts;

namespace MemoGlobal_BackendHomeTest.UnitOfWorkPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersContext context;

        public UnitOfWork(UsersContext context )
        {
            this.context = context;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
