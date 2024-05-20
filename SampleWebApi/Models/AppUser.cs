using Microsoft.AspNetCore.Identity;

namespace SampleWebApi.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public virtual  ICollection<Student> Students { get; set; }

    }
}
