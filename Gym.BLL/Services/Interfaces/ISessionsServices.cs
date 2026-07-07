using Gym.BLL.ViewModels.SessionsViewModels;


namespace Gym.BLL.Services.Interfaces
{
    public interface ISessionsServices
    {
        public Task <IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct = default);
    }
}