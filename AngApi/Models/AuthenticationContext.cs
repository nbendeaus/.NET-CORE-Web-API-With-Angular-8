using AngApi.DAL.Model;
using AngApi.DAL.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApi.Models
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options):base(options)
        {

        } 
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ItemDetail> ItemDetails { get; set; }
        public DbSet<PutUp> PutUps { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffViewModel> StaffViewModels { get; set; }
        public DbSet<MovementDetail> MovementDetails { get; set; }
    }
}
