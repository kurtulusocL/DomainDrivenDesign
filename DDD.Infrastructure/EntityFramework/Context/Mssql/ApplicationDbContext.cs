using DDD.Domain.Entities;
using DDD.Domain.Entities.EntityFramework.AppUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.EntityFramework.Context.Mssql
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ExceptionLogger> ExceptionLoggers { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Writer> Writers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var navigation in entityType.GetNavigations())
                {
                    navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
                }
            }

            modelBuilder.Entity<UserSession>().HasOne(us => us.User).WithMany(u => u.UserSessions).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Article_Id").IsUnique();
                entity.HasIndex(e => e.CategoryId).HasDatabaseName("IX_CategoryId");
                entity.HasIndex(e => e.WriterId).HasDatabaseName("IX_WriterId");
                entity.HasIndex(e => e.IsDeleted).HasDatabaseName("IX_Article_IsDeleted");
                entity.HasIndex(e => e.CreatedDate).HasDatabaseName("IX_Article_CreatedDate");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Category_Id").IsUnique();
                entity.HasIndex(e => e.IsDeleted).HasDatabaseName("IX_Category_IsDeleted");
                entity.HasIndex(e => e.CreatedDate).HasDatabaseName("IX_Category_CreatedDate");
            });

            modelBuilder.Entity<Writer>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Writer_Id").IsUnique();
                entity.HasIndex(e => e.IsDeleted).HasDatabaseName("IX_Writer_IsDeleted");
                entity.HasIndex(e => e.CreatedDate).HasDatabaseName("IX_Writer_CreatedDate");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_User_Id").IsUnique();
                entity.HasIndex(e => e.IsDeleted).HasDatabaseName("IX_User_IsDeleted");
                entity.HasIndex(e => e.CreatedDate).HasDatabaseName("IX_User_CreatedDate");
            });

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_UserSession_Id").IsUnique();
                entity.HasIndex(e => e.UserId).HasDatabaseName("IX_UserSession_UserId");
                entity.HasIndex(e => e.IsDeleted).HasDatabaseName("IX_UserSession_IsDeleted");
                entity.HasIndex(e => e.CreatedDate).HasDatabaseName("IX_UserSession_CreatedDate");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Role_Id").IsUnique();
                entity.HasIndex(e => e.IsDeleted).HasDatabaseName("IX_Role_IsDeleted");
                entity.HasIndex(e => e.CreatedDate).HasDatabaseName("IX_Role_CreatedDate");
            });
        }
    }
}
