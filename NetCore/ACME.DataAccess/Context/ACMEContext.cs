namespace ACME.DataAccess.Context
{
    using ACME.DataAccess.Entities;
    using ACME.DataAccess.Entities.Authentication;
    using Microsoft.EntityFrameworkCore;

    public class ACMEContext : DbContext
    {
        public ACMEContext(DbContextOptions<ACMEContext> options) : base(options) { }

        public DbSet<VisitEntity> Visits { get; set; }

        public DbSet<EmployeeEntity> Employees { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<AuthenticationEntity> Authentications { get; set; }

        public DbSet<UserProfileEntity> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisitEntity>()
                .HasOne(v => v.Employee)
                .WithMany(e => e.Visits)
                .HasForeignKey(v => v.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeEntity>()
                .HasBaseType<UserEntity>();
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Authentication)
                .WithOne()
                .HasForeignKey<UserEntity>(u => u.AuthenticationId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Profile)
                .WithOne()
                .HasForeignKey<UserProfileEntity>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
