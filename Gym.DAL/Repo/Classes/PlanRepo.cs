using Gym.DAL.Contexts;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Interfaces;


namespace Gym.DAL.Repo.Classes
{
    public class PlanRepo : GenaricRpo<Plan>, IPlanReositories

    {
        private readonly GymDbContext _context;
        public PlanRepo(GymDbContext dbContext):base(dbContext)
        {
            _context = dbContext;
        }

      
    }
}
