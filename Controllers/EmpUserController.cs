using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhotoSystem.DTO;
using PhotoSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoSystem.Controllers
{
    [Route("api/[controller]")]
    public class EmpUserController : Controller
    {
        private readonly fotodbContext _context;


        public EmpUserController(fotodbContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
       
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public EmpUser Post([FromBody] EmpUser user)
        {
var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = hashedPassword;
            _context.EmpUser.Add(user);

            _context.SaveChanges();
            return user;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
