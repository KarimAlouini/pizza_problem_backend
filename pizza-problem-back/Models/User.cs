using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_problem_back.Models
{
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NbrPizzaLike { get; set; }
    }
}
