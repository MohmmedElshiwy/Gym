using Gym.BLL.Services.Interfaces;
using Gym.BLL.ViewModels.MemberViewModels;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Interfaces;


namespace Gym.BLL.Services.Classes
{
    public class MemberServices : IMemberServices
    {
        private readonly IGenaricRepo<Member> memberRepo;
        private readonly IGenaricRepo<MemberShip> memberShipRepo;
        private readonly IGenaricRepo<Plan> planRepo;
        private readonly IGenaricRepo<HealthRecord> healthRecordRepo;
        private readonly IGenaricRepo<Booking> booking;

        // Get 

        public MemberServices(IGenaricRepo<Member> _memberRepo,IGenaricRepo<MemberShip> memberShipRepo,IGenaricRepo<Plan> planRepo,IGenaricRepo<HealthRecord> healthRecordRepo,IGenaricRepo<Booking> booking)
        {
          memberRepo = _memberRepo;
            this.memberShipRepo = memberShipRepo;
            this.planRepo = planRepo;
            this.healthRecordRepo = healthRecordRepo;
            this.booking = booking;
        }


        public async Task<IEnumerable<MemberViewModel>> GetAllMemberAsync(CancellationToken ct = default)
        {
            var members= await memberRepo.GetAll(false, ct);
            if (!members.Any()) return [];
            var memberViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Photo = m.Photo,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Gender = m.Gender.ToString(),
            });
            return memberViewModels;
        }

        public async Task<MemberViewModel?> GetMemberDetailsAsync(int id, CancellationToken ct = default)
        {
            var member = await memberRepo.GetById(id, ct);
            if (member is null) return null;

            var memberViewModel = new MemberViewModel
            {
        
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Gender = member.Gender.ToString(),
                Address =
            $"{member.Address?.BuildNumber??null } - {member.Address?.Street ?? ""} - {member.Address?.City ?? ""}".Trim(' ', '-')
            };


            var activeMemberShip = await memberShipRepo.FirstOrDefaultAsync(mb=>mb.MemberId == id && mb.EndDate > DateTime.Now, ct,false);
            if(activeMemberShip is not null)
            {
                var plan = await planRepo.GetById(activeMemberShip.PlanId, ct);
                if (plan is not null)
                {
                    memberViewModel.PlanName = plan.Name;
                    memberViewModel.MembershipStartDate = activeMemberShip.CreatedAt.ToShortDateString();
                    memberViewModel.MembershipEndDate = activeMemberShip.EndDate.ToShortDateString();
                }

            }
            return memberViewModel;
        }
        public async Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int id, CancellationToken ct = default)
        {
            var record = await healthRecordRepo.FirstOrDefaultAsync(r => r.MemberId == id, ct);
            if(record is null) return null;
            return new HealthRecordViewModel()
            {
             
              
                Height= record.Height,
                Weight= record.Weight,
                BloodType= record.BloodType,
                Note= record.Note
            };
        }

        public async Task<MemberToUpdateViewModel> GetMembeToUpdateAsync(int id, CancellationToken ct = default)
        {
            var member =await memberRepo.GetById(id, ct);
            if(member is null) return null;
            return new MemberToUpdateViewModel()
            {
               
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address?.BuildNumber,
                City = member.Address?.City,
                Street = member.Address?.Street
            };
        }







        // Post 
        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            var emailIsExist = await memberRepo.AnyAsync(m => m.Email == model.Email, ct);

            var phoneIsExist = await memberRepo.AnyAsync(m => m.Phone == model.Phone, ct);
            if (emailIsExist || phoneIsExist) return false;
            var member = new Member()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address()
                {
                    BuildNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                HealthRecord = new HealthRecord()
                {
                    Height = model.HealthRecordViewModel.Height,
                    Weight = model.HealthRecordViewModel.Weight,
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Note = model.HealthRecordViewModel.Note
                }

            };
            memberRepo.Add(member);
            var result = await memberRepo.SaveChangesAsync(ct);
            return result > 0;
        }

        public async Task<bool> UpdateMemberDetailsAsync(int id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            var member = await memberRepo.GetById(id, ct);
            if(member is null) return false;
            if (await memberRepo.AnyAsync(m => m.Email == model.Email && m.Id != id)) return false;
            if (await memberRepo.AnyAsync(m => m.Phone == model.Phone && m.Id != id)) return false;
         
            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address?.City = model.City;
            member.Address?.Street = model.Street;
            member.Address?.BuildNumber = model.BuildingNumber;
            member.UpdatedAt= DateTime.Now;


            memberRepo.update(member);
            var result = await memberRepo.SaveChangesAsync(ct);
            return result > 0;


        }
        public async Task<bool> DeleteMemberAsync(int id, CancellationToken ct = default)
        {
       
            var hasFutuerSession  = await booking.AnyAsync(b => b.MemberId == id && b.Session.EndTime > DateTime.Now, ct);
            if (hasFutuerSession) return false;
            memberRepo.Delete(id);
            var result = await memberRepo.SaveChangesAsync(ct);
              return result > 0;
        }
    }
}
