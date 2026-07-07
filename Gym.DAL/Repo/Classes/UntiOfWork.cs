using Gym.DAL.Contexts;
using Gym.DAL.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DAL.Repo.Classes
{
    public class UntiOfWork : IUnitOfWork
    {
        private readonly GymDbContext dbContext;
        private readonly Dictionary<string, object> _Repos=[];
        public UntiOfWork(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
            SessionsRepo= new SessionRepo(dbContext);
        }

        public ISessionRepo SessionsRepo { get; }

        public IGenaricRepo<T> GetRepo<T>() where T : BaseEntity, new()
        {
            var typeName = typeof(T).Name;
            if (_Repos.TryGetValue(typeName, out var OldRepo))
                return (IGenaricRepo<T>)OldRepo;

            var newRepo = new GenaricRpo<T>(dbContext);
            _Repos[typeName]= newRepo;
            return newRepo;

        }
            

        public async Task<int> SaveChangesAsync(CancellationToken ct)
        {
          return  await dbContext.SaveChangesAsync(ct);
        }
    }
}