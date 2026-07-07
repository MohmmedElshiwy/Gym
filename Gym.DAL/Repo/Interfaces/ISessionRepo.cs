using Gym.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DAL.Repo.Interfaces
{
    public interface ISessionRepo:IGenaricRepo<Session>
    {
        Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategoryAsync(CancellationToken ct);
        Task<Session?> GetlSessionWithTrainerAndCategoryAsync(int id,CancellationToken ct);
        Task<int> GetCountOfBookedSlotsAsync(int sessionId, CancellationToken ct);
    }
}