namespace ACME.DataAccess.Repositories
{
    using ACME.DataAccess.Context;
    using Microsoft.EntityFrameworkCore;
    public class ACMERepository<TEntity, TId> : EfRepository<TEntity, TId> where TEntity : class, new()
    {
        protected readonly DbSet<TEntity> DbSet;

        public ACMERepository(ACMEContext context) : base(context)
        {
            DbSet = context.Set<TEntity>();
        }
    }
}
