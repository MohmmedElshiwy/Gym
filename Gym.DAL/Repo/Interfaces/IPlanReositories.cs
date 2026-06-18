using Gym.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DAL.Repo.Interfaces
{
    public interface IPlanReositories
    {
        Task<IEnumerable<Plan>> GetAll();
        Task<Plan?> GetById(int id);
        void Add(Plan plan);
        void Update(Plan plan);
        void Delete(int id);
        Task<int> SaveChangesAsync();

    }
}
