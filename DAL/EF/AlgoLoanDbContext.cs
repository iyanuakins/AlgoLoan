namespace DAL.EF
{
    using System.Data.Entity;

    public partial class AlgoLoanDbContext : DbContext
    {
        public AlgoLoanDbContext()
            : base("name=AlgoLoanConnStr")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Search> Searches { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provider>()
                .Property(e => e.minRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Provider>()
                .Property(e => e.maxRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<Search>()
                .Property(e => e.amount)
                .HasPrecision(18, 8);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Subscriptions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
