﻿using ORM.Model;
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

            modelBuilder.Entity<Subject>().HasMany(s => s.Tests).WithRequired(t => t.Subject).WillCascadeOnDelete(false);

            modelBuilder.Entity<Test>()
                .HasMany(t => t.Questions)
                .WithMany(q => q.Tests)
                .Map(m => { m.ToTable("QuestionsInTests"); m.MapLeftKey("TestId"); m.MapRightKey("QuestionId");});

            modelBuilder.Entity<Question>().HasMany(q => q.Answers).WithRequired(a => a.Question).Map(m => m.MapKey("QuestionId"));
            modelBuilder.Entity<Question>().HasMany(q => q.Fakes).WithRequired(f => f.Question).Map(m => m.MapKey("QuestionId"));
        }
    }
}
