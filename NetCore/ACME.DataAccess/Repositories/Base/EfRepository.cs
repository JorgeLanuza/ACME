namespace ACME.DataAccess.Repositories
{
    using ACME.DataAccess.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using System.Reflection;

    public class EfRepository<T, TId> : IEfRepository<T, TId>, IDisposable where T : class, new()
    {
        protected readonly ACMEContext _context;

        protected readonly DbSet<T> _dbSet;

        private bool _disposed;

        public EfRepository(ACMEContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<ICollection<T>> GetAllNotDeletedAsync()
        {
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                return await _dbSet.Where(e => EF.Property<bool>(e, "IsDeleted") == false).ToListAsync();
            }
            return await _dbSet.ToListAsync();
        }
        public virtual async Task<ICollection<T>> GetAllAsync()
        {
                return await _dbSet.ToListAsync();
        }
        public virtual async Task<T> GetByIdAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return null;
            if (typeof(T).GetProperty("IsDeleted") != null &&
                (bool)typeof(T).GetProperty("IsDeleted").GetValue(entity) == true)
            {
                return null;
            }
            return entity;
        }
        public virtual async Task<T> Get(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return null;
            if (typeof(T).GetProperty("IsDeleted") != null &&
                (bool)typeof(T).GetProperty("IsDeleted").GetValue(entity) == true)
            {
                return null;
            }
            return entity;
        }
        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task<int> SaveAsync()
        {
                return await _context.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return;
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                typeof(T).GetProperty("IsDeleted").SetValue(entity, true);
                await _context.SaveChangesAsync();
            }
            else
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> RemoveRangeAsync(IEnumerable<T> entities)
        {
                _dbSet.RemoveRange(entities);
                return await _context.SaveChangesAsync();
        }
        public virtual IDbContextTransaction BeginTransaction()
        {
                return _context.Database.BeginTransaction();
        }
        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
