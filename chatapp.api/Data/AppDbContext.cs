using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatapp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace chatapp.api.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<AppUser> AspNetUsers { get; set; }
    }
}