using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IFMAMVCDemo.Data.Models;

namespace IFMAMVCDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public static readonly ILoggerFactory ConsoleLogger
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Not for production, show EF query data in the console
        //    optionsBuilder.UseLoggerFactory(ConsoleLogger);
        //    // Show EF Query parameter values in the console
        //    optionsBuilder.EnableSensitiveDataLogging();
        //    base.OnConfiguring(optionsBuilder);
        //}

        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
