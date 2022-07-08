using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class PostUnitTestController
    {
        private PostRepository repository;
        private ISieveProcessor processor;
        public static DbContextOptions<Context> dbContextOptions { get; }
        public static string connectionString = "Server=.;Database=TestUnitDB;integrated Security=true";

        static PostUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public PostUnitTestController()
        {
            var context = new Context(dbContextOptions);
            DummyDataInitializer db = new DummyDataInitializer();
            db.seed(context);

            repository = new PostRepository(context , processor);

        }
    }
}
