using Gym.DAL.Contexts;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Gym.DAL.Repo.Classes
{
    public class PlanRepo : IPlanReositories

    {
        private readonly GymDbContext _context;
        public PlanRepo(GymDbContext dbContext)
        {
            _context = dbContext;
        }

        public void Add(Plan plan)
        {
          _context.Plans.Add(plan);
        }

        public void Delete(int id)
        {
            var product = _context.Plans.FirstOrDefault(p => p.Id == id);
            if(product!= null)
            _context.Plans.Remove(product);
        }

        public async Task<IEnumerable<Plan>> GetAll()
        {
         return await _context.Plans.ToListAsync();
        }

        public async Task<Plan?> GetById(int id)
        {
            return await _context.Plans.FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Plan plan)
        {
             _context.Plans.Update(plan);
        }
    }
}
