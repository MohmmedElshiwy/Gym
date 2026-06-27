using Gym.DAL.Contexts;
using Gym.DAL.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Gym.DAL.Repo.Classes
{
    public class GenaricRpo<T> : IGenaricRepo<T> where T : BaseEntity, new()
    {
        private readonly GymDbContext dbContext;

        public GenaricRpo(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return await dbContext.Set<T>().AnyAsync(predicate, ct);
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<T>().FirstOrDefault(i => i.Id == id);
            if (item != null) dbContext.Set<T>().Remove(item);
            
        }


        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default, bool istraked = false)
        {
            var items = istraked ? dbContext.Set<T>() : dbContext.Set<T>().AsNoTracking();
            return await items.FirstOrDefaultAsync(predicate,ct);
        }

        public async Task<IEnumerable<T>> GetAll(bool istraked, CancellationToken ct = default)
        {
            var items = istraked ? dbContext.Set<T>() : dbContext.Set<T>().AsNoTracking();
            return await items.ToListAsync(ct);
        }

        public async Task<T?> GetById(int id,  CancellationToken ct = default)
        {
            var item = await dbContext.Set<T>().FirstOrDefaultAsync(i
                => i.Id == id);
            return item;

        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return dbContext.SaveChangesAsync(ct);
        }

        public void update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }
    }
}
