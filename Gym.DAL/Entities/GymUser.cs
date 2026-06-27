
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gym.DAL.Entities
{
    public class GymUser:BaseEntity
    {
         public string Name { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^01[0125][0-9]{8}$")]
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
         public Gender Gender { get; set; }
        public Address? Address { get; set; } 

    }
}

[Owned]
public class Address
{
    public int? BuildNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }

}