using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza_problem_back.Data;
using pizza_problem_back.Interfaces;

namespace pizza_problem_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IManageLike _manageLike;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PizzaController(DataContext context, IManageLike manageLike, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _manageLike = manageLike;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPizza()
        {
            var pizzas = await _context.Pizzas.ToListAsync();
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizzaById(int id)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(pizza);
        }

        [Authorize]
        [HttpPost("{idPizza}")]
        public async Task<IActionResult> LikePizzaById(int idPizza)
        {
            var userConnected = _httpContextAccessor.HttpContext.User.Identity;
            var result = await _manageLike.LikePizza(idPizza, userConnected.Name);
            return Ok(result);
        }
    }
}
