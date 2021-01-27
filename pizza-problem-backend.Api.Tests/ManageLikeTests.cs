using Microsoft.EntityFrameworkCore;
using pizza_problem_back.Data;
using pizza_problem_back.Models;
using pizza_problem_back.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace pizza_problem_backend.Api.Tests
{
    public class ManageLikeTests
    {
        [Fact]
        public async void LikePizza_returnOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "PizzaDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DataContext(options))
            {
                context.Users.Add(new User { Id = 7, UserName = "user 1", NbrPizzaLike = 20 });
                context.Users.Add(new User { Id = 8, UserName = "user 2", NbrPizzaLike = 15 });
                context.Users.Add(new User { Id = 9, UserName = "user 3", NbrPizzaLike = 30 });
                context.SaveChanges();
            }

            //Act
            using(var context = new DataContext(options))
            {
                ManageLike manageLike = new ManageLike(context);
                await manageLike.LikePizza("user 1");

                //Assert
                Assert.Equal(21, context.Users.FirstOrDefault(x => x.UserName.Equals("user 1")).NbrPizzaLike);
            }
        }
    }
}
