using Core.Domain.Commons;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class SocialMediaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public  DbSet<Comment> Comments { get; set; }

        public DbSet<Friends> Friends { get; set; }

        public SocialMediaContext(DbContextOptions options) : base(options) { }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy="";
                        entry.Entity.Updated = DateTime.Now;
                        entry.Entity.UpdatedBy = "";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Comment>().ToTable("Comments");

            modelBuilder.Entity<Friends>().ToTable("Friends");

            #endregion
            #region Primary Key
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<Post>().HasKey(post => post.Id);
            modelBuilder.Entity<Comment>().HasKey(comment => comment.Id);
            modelBuilder.Entity<Friends>().HasKey(comment => comment.Id);

            #endregion
            #region Relationship
            #region Post

            modelBuilder.Entity<Post>()
                .HasMany<Comment>(post => post.Comments)
                .WithOne(comment => comment.Post)
                .HasForeignKey(comment => comment.IdPost)
                .OnDelete(DeleteBehavior.Cascade);
                
            #endregion
            #region User
            modelBuilder.Entity<User>()
                .HasMany<Post>(user => user.Posts)
                .WithOne(post => post.User)
                .HasForeignKey(post => post.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Comment>(user => user.Comments)
                .WithOne(comment => comment.User)  
                .HasForeignKey(comment => comment.IdUser)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany<Friends>(user => user.Friend)
                .WithOne(comment => comment.User)
                .HasForeignKey(comment=> comment.IdUser)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany<Friends>(user => user.FriendOf)
                .WithOne(comment => comment.Friend)
                .HasForeignKey(comment => comment.IdFriend)
                .OnDelete(DeleteBehavior.NoAction);
        


            #endregion
            #endregion
        }

    }
}
