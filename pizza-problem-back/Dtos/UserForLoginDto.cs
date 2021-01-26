using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_problem_back.Dtos
{
    public class UserForLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
