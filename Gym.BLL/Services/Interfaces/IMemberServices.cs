using Gym.BLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BLL.Services.Interfaces
{
    public interface IMemberServices
    {
        //Get  Model => ViewModel => View
        Task<IEnumerable<MemberViewModel>> GetAllMemberAsync(CancellationToken ct = default);
      
        Task<MemberViewModel?> GetMemberDetailsAsync(int id, CancellationToken ct = default);
        Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int id, CancellationToken ct = default);



        Task<MemberToUpdateViewModel> GetMembeToUpdateAsync(int id, CancellationToken ct = default);

        //Post ViewModel =>Model=>DB
        Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default);
        Task<bool> UpdateMemberDetailsAsync(int id, MemberToUpdateViewModel model, CancellationToken ct = default);
        Task<bool> DeleteMemberAsync(int id, CancellationToken ct = default);
    }
}
