using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gym.DAL.Repo.Interfaces
{
    public interface IGenaricRepo<T> where T:BaseEntity,new()
    {
        Task <IEnumerable<T>> GetAll(bool istraked ,CancellationToken ct = default);

        Task <T?> GetById(int id,  CancellationToken ct = default);
        void Add(T entity);
        void update(T entity);
        void Delete(int id);
        Task <int> SaveChangesAsync(CancellationToken ct = default);
        Task <T?>  FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,CancellationToken ct = default,bool istraked = false);
        Task <bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);

    }
}
