using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KinneretCollegeTimeSheet.Models;

namespace KinneretCollegeTimeSheet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<WebSiteLanguages> Language { get; set; }
        public virtual DbSet<UserCourse> userCourse { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<Student> Student { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
