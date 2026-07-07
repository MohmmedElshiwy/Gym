using Gym.BLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BLL.Services.Interfaces
{
    public interface IPlanServices
    {
        Task<IEnumerable<PlanViewModel>> GetAllPlansAsync(CancellationToken ct);
        Task<PlanViewModel?> GetPlanDetailsAsync(int id, CancellationToken ct);
        Task<UpdatePlanViewModel?> GetPlanToUpdateAsync(int id, CancellationToken ct);
        Task<bool> UpdatedPlanAsync(int id, UpdatePlanViewModel plan, CancellationToken ct);
        Task<bool> IsDeletedAsync(int id, CancellationToken ct);
    }
}
