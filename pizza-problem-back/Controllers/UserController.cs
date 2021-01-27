using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza_problem_back.Data;
using pizza_problem_back.Interfaces;
using pizza_problem_back.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_problem_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IManageLike _manageLike;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(DataContext context, IManageLike manageLike, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _manageLike = manageLike;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {

            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idPizza"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("likePizza")]
        public async Task<IActionResult> LikePizza()
        {
            try
            {
                var userConnected = _httpContextAccessor.HttpContext.User.Identity;
                await _manageLike.LikePizza(userConnected.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("UsersLike")]
        public async Task<IActionResult> GetMostUsersLiked()
        {
            var userLike = from user in _context.Users
                           select new UserLike
                           {
                               Name = user.UserName,
                               NumberOfLike = user.NbrPizzaLike
                           };

            var users = await userLike.Take(10).OrderByDescending(x => x.NumberOfLike).ToListAsync();
            return Ok(users);
        }
    }
}
