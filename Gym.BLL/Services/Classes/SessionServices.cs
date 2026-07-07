using AutoMapper;
using Gym.BLL.Services.Interfaces;
using Gym.BLL.ViewModels.SessionsViewModels;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Interfaces;

namespace Gym.BLL.Services.Classes
{
    public class SessionServices : ISessionsServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SessionServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct )
        {
           var sessions =await unitOfWork.SessionsRepo.GetAllSessionsWithTrainerAndCategoryAsync(ct);
            if (!sessions.Any()) return null;

            sessions = sessions.OrderByDescending(s => s.StartTime);
            //var MapSessions = sessions.Select(s => new SessionViewModel
            //{
            //    Id = s.Id,
            //    Description = s.Description,
            //    Capacity = s.Capacity,
            //    StartDate = s.StartTime,
            //    EndDate = s.EndTime,
            //    TrainerName = s.Trainer.Name,
            //    CategoryName = s.Category.CategoryName,
            //    AvailableSlots = s.Capacity - s.Bookings.Count(),
            //});
            var MapSessions= mapper.Map<IEnumerable<Session>, IEnumerable < SessionViewModel >> (sessions);
;            foreach (var session in MapSessions)
            {
                session.AvailableSlots=session.Capacity - await unitOfWork.SessionsRepo.GetCountOfBookedSlotsAsync(session.Id, ct);
            }
            return MapSessions;
        }
    }
}