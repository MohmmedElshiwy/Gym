using Gym.BLL.Services.Interfaces;
using Gym.BLL.ViewModels.PlanViewModels;
using Gym.DAL.Contexts;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Interfaces;

namespace Gym.BLL.Services.Classes
{
    public class PlanServices : IPlanServices
    {
        private readonly IGenaricRepo<Plan> planRepo;
        private readonly IGenaricRepo<MemberShip> memberShipRepo;

        public PlanServices(IGenaricRepo<Plan> _planRepo, IGenaricRepo<MemberShip> _memberShipRepo)
        {
            planRepo = _planRepo;
            memberShipRepo = _memberShipRepo;
        }
        public async Task<IEnumerable<PlanViewModel>> GetAllPlansAsync(CancellationToken ct)
        {
            var plans = await planRepo.GetAll(false,ct);
            if (!plans.Any()) return [];
            var planViewModels = plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Price = p.Price,
                IsActive = p.IsActive
            });
            return planViewModels;


        }

        public async Task<PlanViewModel?> GetPlanDetailsAsync(int id, CancellationToken ct)
        {
            var plan = await planRepo.GetById(id, ct);
            if (plan == null) return null;
            var planViewModel = new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive
            };
            return planViewModel;
        }

        public async Task<UpdatePlanViewModel?> GetPlanToUpdateAsync(int id, CancellationToken ct)
        {
            var plan =await planRepo.GetById(id, ct);
            if (plan == null) return null;
            return new UpdatePlanViewModel
            {
              PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price

            };
        }
      

        public async Task<bool> UpdatedPlanAsync(int id, UpdatePlanViewModel model, CancellationToken ct)
        {
            var plan = await planRepo.GetById(id, ct);
            if (plan is null) return false;
            plan.Name = model.PlanName;
            plan.Description = model.Description;
            plan.DurationDays = model.DurationDays;
            plan.Price = model.Price;
            planRepo.update(plan);
            var result = await planRepo.SaveChangesAsync(ct);
            return result > 0;
        }
        public async Task<bool> IsDeletedAsync(int id, CancellationToken ct)
        {
            var planHasMemberShip = await memberShipRepo.AnyAsync(ms => ms.PlanId==id && ms.EndDate>DateTime.Now,ct);
            if (planHasMemberShip) return false;
            var plan =await planRepo.GetById(id,ct);
            if (plan is null) return false;
            plan.IsActive = !plan.IsActive;
            planRepo.update(plan);
           
            var result = await planRepo.SaveChangesAsync(ct);
            return result > 0;

        }

    }
}
