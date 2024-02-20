using AdminDashboard.Data.Models;
using AdminDashboard.Data.Models.Categories;
using AdminDashboard.Data.Models.Members;
using AdminDashboard.Data.Models.Payments;
using AdminDashboard.Data.Models.Titles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdminDashboard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public static readonly ILoggerFactory ConsoleLogger
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Not for production, show EF query data in the console
            optionsBuilder.UseLoggerFactory(ConsoleLogger);
            // Show EF Query parameter values in the console
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

       
        public DbSet<Category> Categories { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
