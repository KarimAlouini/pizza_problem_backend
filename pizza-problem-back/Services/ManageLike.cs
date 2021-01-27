using Microsoft.EntityFrameworkCore;
using pizza_problem_back.Data;
using pizza_problem_back.Interfaces;
using pizza_problem_back.Models;
using System;
using System.Threading.Tasks;

namespace pizza_problem_back.Services
{
    public class ManageLike : IManageLike
    {
        private readonly DataContext _context;

        public ManageLike(DataContext context)
        {
            _context = context;
        }

        public async Task LikePizza(string username)
        {
            User userConnected = await _context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(username));
            userConnected.NbrPizzaLike++;
            _context.Update(userConnected);
            await _context.SaveChangesAsync();
        }
    }
}
