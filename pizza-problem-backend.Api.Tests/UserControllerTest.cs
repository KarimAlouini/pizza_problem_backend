using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using pizza_problem_back.Controllers;
using pizza_problem_back.Data;
using pizza_problem_back.Interfaces;
using pizza_problem_back.Models;
using Xunit;

namespace pizza_problem_backend.Api.Tests
{
    public class UserControllerTest
    {
        //protected UserController userController;
        protected IHttpContextAccessor _httpContextAccessor;
        protected Mock<IManageLike> _manageLike;

        public UserControllerTest()
        {
            _manageLike = new Mock<IManageLike>();
        }

        [Fact]
        public async void LikePizza_WhenUserIsConnected_ReturnsOkAsync()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "PizzaDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DataContext(options))
            {
                context.Users.Add(new User { Id = 4, UserName = "user a", NbrPizzaLike = 20 });
                context.Users.Add(new User { Id = 5, UserName = "user b", NbrPizzaLike = 15 });
                context.Users.Add(new User { Id = 6, UserName = "user c", NbrPizzaLike = 30 });
                context.SaveChanges();
            }

            //Mock IHttpContextAccessor
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpcontext = new DefaultHttpContext();
            var fakeUsername = "abcd";
            httpcontext.Request.Headers["User"] = fakeUsername;
            mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(httpcontext);

            //Act
            using (var context = new DataContext(options))
            {
                UserController userController = new UserController(context, _manageLike.Object, mockHttpContextAccessor.Object);
                var exception = await Record.ExceptionAsync(async () => await userController.LikePizza());

                Assert.Null(exception);
            }
        }

        [Fact]
        public async void GetAllUser()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "PizzaDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DataContext(options))
            {
                context.Users.Add(new User { Id = 1, UserName = "user 1", NbrPizzaLike = 20 });
                context.Users.Add(new User { Id = 2, UserName = "user 2", NbrPizzaLike = 15 });
                context.Users.Add(new User { Id = 3, UserName = "user 3", NbrPizzaLike = 30 });
                context.SaveChanges();
            }

            //Act
            using (var context = new DataContext(options))
            {
                UserController userController = new UserController(context, _manageLike.Object, _httpContextAccessor);
                IActionResult users = await userController.GetAllUser().ConfigureAwait(false);

                //Assert
                Assert.NotNull(users);
                Assert.IsType<OkObjectResult>(users);
            }
        }
    }
}
