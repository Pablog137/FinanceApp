using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Data
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
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Transfer> Transfers { get; set; }



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

            modelBuilder.Entity<Transfer>()
            .HasOne(t => t.SenderAccount)
            .WithMany()
            .HasForeignKey(t => t.SenderAccountId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.RecipientAccount)
                .WithMany()
                .HasForeignKey(t => t.RecipientAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .Property(p => p.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Notification>()
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