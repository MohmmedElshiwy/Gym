using Gym.DAL.Contexts;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DAL.Repo.Classes
{
    internal class MemberRepo : GenaricRpo<Member>, IMemberRepo
    {
        public MemberRepo(GymDbContext dbContext) : base(dbContext)
        {
        }


    }
}
