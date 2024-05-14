using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormType> FormTypes { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email).IsRequired();

                // Configure relationship between User and Role (one-to-many)
                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.RoleID);

                // Configure relationship between User and Forms (one-to-many)
                entity.HasMany(u => u.Forms)
                      .WithOne(f => f.User)
                      .HasForeignKey(f => f.UserID);

                // Configure relationship between User and Salary (one-to-one)
                entity.HasOne(u => u.Salary)
                      .WithOne(s => s.User)
                      .HasForeignKey<Salary>(s => s.UserID);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                // Configure relationship between Role and Users (one-to-many)
                entity.HasMany(r => r.Users)
                      .WithOne(u => u.Role)
                      .HasForeignKey(u => u.RoleID);

                // Configure relationship between Role and Claims (one-to-many)
                entity.HasMany(r => r.Claims)
                      .WithOne(c => c.Role)
                      .HasForeignKey(c => c.RoleID);
            });

            modelBuilder.Entity<Form>(entity =>
            {
                // Configure relationship between Form and User (one-to-many)
                entity.HasOne(f => f.User)
                      .WithMany(u => u.Forms)
                      .HasForeignKey(f => f.UserID);

                // Configure relationship between Form and FormType (many-to-one)
                entity.HasOne(f => f.FormType)
                      .WithMany(t => t.Forms)
                      .HasForeignKey(f => f.TypeID);

                // Configure attachments in form
                entity.Property(f => f.Attachments)
                .HasColumnType("varbinary(max)");

                entity.Property(f => f.CreatedDate)
              .HasDefaultValueSql("GETDATE()");
            });

            // FormType configuration
            modelBuilder.Entity<FormType>(entity =>
            {
                // Configure relationship between FormType and Forms (one-to-many)
                entity.HasMany(t => t.Forms)
                      .WithOne(f => f.FormType)
                      .HasForeignKey(f => f.TypeID);
            });

            // Claim configuration
            modelBuilder.Entity<Claim>(entity =>
            {

            });

            // UserClaim configuration (many-to-many relationship)
            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasKey(uc => new { uc.UserID, uc.ClaimID });

                // Configure relationship between UserClaim and User (many-to-many)
                entity.HasOne(u => u.User)
                      .WithMany(uc => uc.UserClaims)
                      .HasForeignKey(u => u.UserID);

                // Configure relationship between UserClaim and Claim (many-to-many)
                entity.HasOne(c => c.Claim)
                      .WithMany(uc => uc.UserClaims)
                      .HasForeignKey(c => c.ClaimID);
            });

            // Salary configuration
            modelBuilder.Entity<Salary>(entity =>
            {
                // Configure relationship between Salary and User (one-to-one)
                entity.HasOne(s => s.User)
                      .WithOne(u => u.Salary)
                      .HasForeignKey<Salary>(s => s.UserID);
            });
        }
    }
}