using Hospital_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_App.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string UserRole { get; set; }

        public DateTime RegisteredDate { get; set; }
        public string Status { get;set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public string UserRole { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Status { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<OfficeManager> OfficeManagers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }
        public DbSet<Chronic> Chronics { get; set; }
        public DbSet<Contract>Contracts { get; set; }
        public DbSet<PatientChronic> PatientChronics { get; set; }
        public DbSet<PreferedSuburb> PreferedSuburbs { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Schedule>Schedules { get; set; }
        

    }
}
