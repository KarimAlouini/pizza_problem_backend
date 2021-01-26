using Microsoft.EntityFrameworkCore;
using pizza_problem_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_problem_back.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
