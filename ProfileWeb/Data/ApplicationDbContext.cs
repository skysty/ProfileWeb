using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProfileWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>

    {
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
        public DbSet<Kafedra> Kafedras { get; set; }
        public DbSet<Ranks> Ranks { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<Workway> Workways { get; set; }
        public DbSet<Qulification> Qulifications { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        
    }
}
