
namespace Gym.DAL.Entities
{
    public class HealthRecord:BaseEntity
    {
        public float Weight { get; set; }
        public float Height { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }
        public Member Member { get; set; } = null!;
        public int MemberId { get; set; }

    }
}
