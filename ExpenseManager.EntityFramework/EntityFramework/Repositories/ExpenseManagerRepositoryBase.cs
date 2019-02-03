using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace ExpenseManager.EntityFramework.Repositories
{
    public abstract class ExpenseManagerRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ExpenseManagerDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ExpenseManagerRepositoryBase(IDbContextProvider<ExpenseManagerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class ExpenseManagerRepositoryBase<TEntity> : ExpenseManagerRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ExpenseManagerRepositoryBase(IDbContextProvider<ExpenseManagerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
