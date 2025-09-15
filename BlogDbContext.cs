using Blog.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        //first add tables that have,'t foreign key relation
        public DbSet<User> Users { get; set; } // table name
        public DbSet<Category> Categories { get; set; } // table name

        public DbSet <Post> Posts { get; set; } // table name 
        public DbSet<Comment> Comments { get; set; } // table name

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //user

            //modelBuilder.Entity<User>(entity =>
            //{ //pk
            //    entity.HasKey(user => user.Id);
            //    //username
            //    entity.Property(user => user.Username).IsRequired()
            //    .HasMaxLength(150);

            //    //email
            //    entity.HasIndex(user => user.Email).IsUnique();



            //}
            //);

            //modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();





            //relations 
            // 1-1   1-m   m-m
            // post , comment  1-M
            //modelBuilder.Entity<Post>().HasMany(post => post.Comments)
            //     .WithOne(comment => comment.MyPost)
            //     .HasForeignKey(comment => comment.PostId);

            ////post , category  M-M

            //modelBuilder.Entity<Post>().HasMany(post => post.MyCategory).
            //    withMany(category => category.Posts).
            //    usingEntity(entity => entity.ToTable("PostCategories"));


        }

    }
}
