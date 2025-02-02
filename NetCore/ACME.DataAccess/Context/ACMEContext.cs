namespace ACME.DataAccess.Context
{
    using ACME.DataAccess.Entities;
    using ACME.DataAccess.Entities.Authentication;
    using Microsoft.EntityFrameworkCore;

    public class ACMEContext(DbContextOptions<ACMEContext> options) : DbContext(options)
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<VisitEntity> Visits { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserProfileEntity> UserProfiles { get; set; }
        public DbSet<AuthenticationEntity> Authentications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureDb(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private static void ConfigureDb(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>()
                .HasMany(e => e.Visits)
                .WithOne(v => v.Employee)
                .HasForeignKey(v => v.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Authentication)
                .WithOne()
                .HasForeignKey<UserEntity>(u => u.AuthenticationId);

            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Profile)
                .WithOne()
                .HasForeignKey<UserEntity>(u => u.Id);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
