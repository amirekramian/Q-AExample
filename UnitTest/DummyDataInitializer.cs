using DataAccess.Context;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class DummyDataInitializer
    {
        public DummyDataInitializer()
        {

        }
        public void seed(Context context)
        {
            context.Database.EnsureCreated();
            context.Database.EnsureDeleted();

            context.Users.AddRange(new User
            {
                ID = 1,
                InsertedDateTime = DateTime.Now,
                UserName = "amir",
                HashedPassword = "amir".GetHashCode().ToString()
            });
            context.Posts.AddRange(new Post
            {
                ID = 1,
                InsertedDateTime = DateTime.Now,
                LikeCount = 5,
                UserID = 1,
                IsDeleted = false
            });
            context.Comments.AddRange(new Comment
            {
                ID = 1,
                Text = "test comment",
                PostID = 1
            });
            context.SaveChanges();
        }
    }
}
