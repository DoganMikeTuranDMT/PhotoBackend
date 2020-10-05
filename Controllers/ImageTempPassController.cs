using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoSystem.Controllers
{
    [Route("api/[controller]")]
    public class ImageTempPassController : Controller
    {
        private readonly fotodbContext _context;


        public ImageTempPassController(fotodbContext context)
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
        [Authorize]
        [HttpPost]
        public void Post([FromBody] ImageTempPass imgtemppass)
        {
            _context.ImageTempPass.Add(imgtemppass);
            
            _context.SaveChanges();
           
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
