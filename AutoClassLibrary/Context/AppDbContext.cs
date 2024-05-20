using AutoClassLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClassLibrary.Context
{
    public class AppDbContext:DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

       public DbSet<Student> Students { get; set; }
    }
}
