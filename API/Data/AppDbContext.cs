using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Contact> Contacts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Account)
                .WithOne(u => u.User)
                .HasForeignKey<Account>(a => a.UserId);

            modelBuilder.Entity<Account>()
           .HasMany(a => a.Contacts)
           .WithMany(c => c.Accounts)
           .UsingEntity<Dictionary<string, object>>(
               "AccountContact",
               j => j.HasOne<Contact>().WithMany().HasForeignKey("ContactId"),
               j => j.HasOne<Account>().WithMany().HasForeignKey("AccountId"));

            modelBuilder.Entity<Transaction>()
                .Property(p => p.Type)
                .HasConversion<string>();


            // Roles
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = 2, Name = "User", NormalizedName = "USER" }
            );
        }
    }
}