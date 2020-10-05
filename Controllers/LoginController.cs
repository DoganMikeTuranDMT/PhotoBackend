using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhotoSystem.DTO;
using PhotoSystem.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoSystem.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly fotodbContext _context;
        private readonly JWTSettings _jwtsettings;

        public LoginController(fotodbContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
        }

        [HttpPost]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] EmpUser user)
        {
            var getUser = _context.EmpUser.Where(e => e.Username == user.Username).Select(r => r.Password).FirstOrDefault();
            bool validPassword = BCrypt.Net.BCrypt.Verify(user.Password, getUser); ;
            
            if (validPassword == false)
            {
                return NotFound();
            }
            user = await _context.EmpUser
                .Where(e => e.Username == user.Username)
                .FirstOrDefaultAsync();

            UserWithToken userWithToken = new UserWithToken(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {

                    new Claim(ClaimTypes.GivenName, user.ClientId.ToString()),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Name),
                    new Claim(JwtRegisteredClaimNames.Email, user.CompanyName),



                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.Token = tokenHandler.WriteToken(token);

            return userWithToken;
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
