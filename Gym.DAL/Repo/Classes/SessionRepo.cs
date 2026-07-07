
using Gym.DAL.Contexts;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gym.DAL.Repo.Classes
{
    public class SessionRepo : GenaricRpo<Session>, ISessionRepo
    {
        private readonly GymDbContext dbContext;

        public SessionRepo(GymDbContext dbContext):base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategoryAsync(CancellationToken ct)
        {
            var sessions = dbContext.Sessions.AsNoTracking()
                .Include(s => s.Trainer)
                .Include(s => s.Category);
                
            return await sessions.ToListAsync(ct);
        }

        public Task<int> GetCountOfBookedSlotsAsync(int sessionId, CancellationToken ct)
        {
            return dbContext.Bookings.AsNoTracking().CountAsync(b => b.SessionId == sessionId);
        }

        public async Task<Session?> GetlSessionWithTrainerAndCategoryAsync(int id, CancellationToken ct)
        {
           var session = dbContext.Sessions.Include(s=>s.TrainerId)
                .Include(s=>s.CategoryId)
                .FirstOrDefaultAsync(s=>s.Id == id, ct);
            return await session;
        }
    }
}