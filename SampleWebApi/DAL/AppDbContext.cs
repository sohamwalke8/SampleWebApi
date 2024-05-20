using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleWebApi.Models;

namespace SampleWebApi.DAL
{
    public class AppDbContext  : IdentityDbContext<AppUser>
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        DbSet<Student> Students { get; set; }
    }
}
