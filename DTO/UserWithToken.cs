using System;
using PhotoSystem.Models;

namespace PhotoSystem.DTO
{
    public class UserWithToken
    {
        public UserWithToken(EmpUser user)
        {
            this.Name = user.Name;
            this.Email = user.Email;
            this.Password = user.Password;
            this.Username = user.Username;
            this.ClientId = user.ClientId;

        }

        public string Token { get; internal set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int ClientId { get; set; }



    }
}
