namespace ACME.DataAccess.Repositories
{
    using Microsoft.EntityFrameworkCore.Storage;
    using System.Threading.Tasks;

    public interface IEfRepository<TModel, TId> : IDisposable
    {
        Task<ICollection<TModel>> GetAllNotDeletedAsync();
        Task<ICollection<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(TId id);
        Task<TModel> Get(TId id);
        Task<int> SaveAsync();
        Task DeleteAsync(TId id);
        Task<int> RemoveRangeAsync(IEnumerable<TModel> entities);
        Task AddAsync(TModel entity);
        Task UpdateAsync(TModel model);
        IDbContextTransaction BeginTransaction();
    }
}
