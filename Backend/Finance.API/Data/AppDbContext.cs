using Finance.API.Models;
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
                .WithOne(a => a.User)
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
                .WithMany(a => a.TransfersAsSender)
                .HasForeignKey(t => t.SenderAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.RecipientAccount)
                .WithMany(a => a.TransfersAsRecipient)
                .HasForeignKey(t => t.RecipientAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .Property(p => p.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Notification>()
                .Property(p => p.Type)
                .HasConversion<string>();

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = 2, Name = "User", NormalizedName = "USER" }
            );
        }

    }
}