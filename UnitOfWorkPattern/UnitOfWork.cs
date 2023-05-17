using MemoGlobal_BackendHomeTest.DBContexts;

namespace MemoGlobal_BackendHomeTest.UnitOfWorkPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersContext context;
        private readonly ILogger<UnitOfWork> logger;

        public UnitOfWork(UsersContext context, ILogger<UnitOfWork> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task Save()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }
    }
}
