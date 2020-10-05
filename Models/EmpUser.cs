using System;
using System.Collections.Generic;

namespace PhotoSystem.Models
{
    public partial class EmpUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int ClientId { get; set; }
        public string Email { get; set; }

        public virtual FoClient Client { get; set; }
    }
}
