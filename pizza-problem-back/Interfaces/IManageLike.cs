using pizza_problem_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_problem_back.Interfaces
{
    public interface IManageLike
    {
        Task LikePizza(string username);
    }
}
