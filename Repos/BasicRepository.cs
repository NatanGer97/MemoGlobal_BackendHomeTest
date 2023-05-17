using MemoGlobal_BackendHomeTest.DBContexts;

namespace MemoGlobal_BackendHomeTest.Repos
{
    public abstract class BasicRepository
    {
        protected UsersContext Context { get; private set; }

        public BasicRepository(UsersContext context)
        {
            Context = context;
        }
    }
}
