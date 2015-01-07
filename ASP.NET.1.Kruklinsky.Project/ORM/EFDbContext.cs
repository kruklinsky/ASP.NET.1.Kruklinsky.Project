using ORM.Model;
using System.Data.Entity;

namespace ORM
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDbConnection") { }

        #region Membership
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        #endregion

        #region Knowledge
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Fake> Fakes { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasRequired(u => u.Profile).WithRequiredPrincipal(p => p.User).Map(m => m.MapKey("UserId"));
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m => { m.ToTable("UsersInRoles"); m.MapLeftKey("UserId"); m.MapRightKey("RoleId"); });
        }
    }
}
