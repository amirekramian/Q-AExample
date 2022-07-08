using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<User>? Users { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Comment>? Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new()
                {
                    ID = 1,
                    UserName="Amir",
                    HashedPassword="1574ad62d48a37f847699d7d2157105a5a5fd6ed323a3497fa41c7731229bf23"

                }
            });
            modelBuilder.Entity<Post>().HasData(new List<Post>
            {
                new()
                {
                    ID=1,
                    UserID=1,
                    Title="first Post",
                    desciption="it is the first post"

                }
            });
            modelBuilder.Entity<Comment>().HasData(new List<Comment>
            {
                new()
                {
                    ID=1,
                    Text="the first comment",
                    CommentLikeCount=0,
                    PostID=1
                    
                    
                }
            });
        }
    }
}
