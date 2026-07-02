using Gym.BLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BLL.Services.Interfaces
{
    public interface ITranierServices
    {
        //get
        Task<IEnumerable<TrainerViewModel>> GetAllTrainerAsync(CancellationToken ct = default);
        Task<TrainerViewModel?> GetTrainerDetailsAsync(int id, CancellationToken ct = default);
        Task<TrainerToUpdateViewModel> GetTrainerToUpdateAsync(int id, CancellationToken ct = default);

        //post
        Task<bool> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken ct = default);
        Task<bool> UpdateTrainerDetailsAsync(int id, TrainerToUpdateViewModel model, CancellationToken ct = default); 
        Task<bool> DeleteTrainerDetailsAsync(int id, CancellationToken ct = default);
    }
}
