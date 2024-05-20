using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleWebApi.DAL;
using SampleWebApi.Models;

namespace SampleWebApi.Utility
{
    public class DBSeeding : IDBSeeding
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppDbContext _context;

        public DBSeeding(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            //Check if default Admin user exists or not
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                //Create User Roles in the database
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Student")).GetAwaiter().GetResult();
              

                //Note: Password should always have one Capital alphabet and one Special Character
                _userManager.CreateAsync(new AppUser
                {
                    UserName = "sohamwalke",
                    Email = "sohamwalke@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                  
                    //Password = "Admin@123"
                }, "Admin@123").GetAwaiter().GetResult();

                //Get the default Admin User, which is created above.
                var Appuser = _context.Users.FirstOrDefault(x => x.Email == "sohamwalke@gmail.com");
                if (Appuser != null)
                {
                    _userManager.AddToRoleAsync(Appuser, "Admin").GetAwaiter().GetResult();
                }
            }
        }

    }


    public interface IDBSeeding
    {
        void Initialize();
    }
}

