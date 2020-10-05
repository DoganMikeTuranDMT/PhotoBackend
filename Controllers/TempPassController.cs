using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PhotoSystem.DTO;
using PhotoSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoSystem.Controllers
{
    [Route("api/[controller]")]
    public class TempPassController : Controller
    {
        private readonly Random _random = new Random();

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(6, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(100000, 999999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(6));
            return passwordBuilder.ToString();
        }
        private readonly fotodbContext _context;
        

        public TempPassController(fotodbContext context)
        {
            _context = context;
            
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("get/{code}")]
        public async Task<IEnumerable<ImageTempPassDTO>> getAllIagesByCode(string code)
        {
            return ( await _context.TempPass.Include(s => s.ImageTempPass).Where(x => x.TempPassword == code).FirstOrDefaultAsync()).ImageTempPass.Select(p => new ImageTempPassDTO {
                ImageLink = p.ImageLink,
                OriginalFileName = p.OriginalFileName,
                TempPasswordId = p.TempPasswordId,
                CustName = p.TempPassword.CustName,
                CustEmail = p.TempPassword.CustEmail,
                UserCompanyName = p.TempPassword.UserCompanyName
            });


        }

        [HttpPost]
        public TempPass Post([FromBody] TempPass temppass)
        {
            _context.TempPass.Add(temppass);
            temppass.TempPassword = RandomPassword();
            _context.SaveChanges();
            return temppass;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{temppasswordid}")]
        public async Task<ActionResult<string>> DeleteImageTempPassAndTempPass(int temppasswordid)
        {
            var allImageTempPassById = _context.ImageTempPass.Where(x => x.TempPasswordId == temppasswordid).ToList();
            foreach (ImageTempPass q in allImageTempPassById)
            {
                _context.Remove(q);
            }
            await _context.SaveChangesAsync();

            _context.Remove(_context.TempPass.Single(e => e.Id == temppasswordid));

           await _context.SaveChangesAsync();

            return "OK";
        }
    }
}
