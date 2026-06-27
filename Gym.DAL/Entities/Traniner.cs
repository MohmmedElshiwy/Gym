using Gym.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.DAL.Entities
{
    public class Traniner: GymUser
    {
        public Specialties Specialties { get; set; }

        public DateTime HiringDate { get; set; }
        public ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
    }
}
