using System;

namespace SIMS_IT0602.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public String Pass { get; set; }
        public string Role { get; set; }

        public User()
        {
        }
    }
}
