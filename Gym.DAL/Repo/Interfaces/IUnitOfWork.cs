
namespace Gym.DAL.Repo.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenaricRepo<T> GetRepo<T>() where T : BaseEntity, new();
        public ISessionRepo SessionsRepo { get; }
     

        public Task<int> SaveChangesAsync(CancellationToken ct);
    }
}