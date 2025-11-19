using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebAPIService.Models;
using WebAPIService.Helpers;

namespace WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoryPersonsController : Controller
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "MemoryPersons";
        public MemoryPersonsController(IMemoryCache cache)
        {
            _cache = cache;
        }

        //api/Controller/GetMemoryPerson?ID=x
        [HttpGet("GetMemoryPerson")]
        //api/Controller/GetMemoryPerson/x
        [HttpGet("GetMemoryPerson/{id}")]
        public async Task<ActionResult<MemoryPersons>> GetMemoryPerson(int? id)
        {
            await Task.Yield();
            // if ID was provided instead of id
            if (id == null && Request.Query.ContainsKey("ID"))
                id = int.Parse(Request.Query["ID"]);

            if (id == null)
                return BadRequest("id is required.");

            var persons = _cache.Get<List<MemoryPersons>>(CacheKey);
            if(persons == null) 
                return NotFound("No Persons Stored.");

            var person = persons.FirstOrDefault(x => x.Id == id);

            if (person == null)
                return NotFound($"Person with ID {id} not found.");

            return Ok(person);
        }

        [HttpGet("LoginMemoryPerson")]
        public async Task<ActionResult<MemoryPersons>> Login(string id, string password)
        {
            await Task.Yield();
            if (id == null)
                return BadRequest("id is required.");
            if (password == null)
                return BadRequest("password is required.");
            var persons = _cache.Get<List<MemoryPersons>>(CacheKey);
            if(persons == null)
                return NotFound("User not registered");
            var hashpassword = HashCalculation.computeSha1Hash(password);
            var person = persons.FirstOrDefault(x => x.loginID == id && x.password == hashpassword);

            if (person == null)
                return NotFound($"Login Incorrect ID/Password");

            return Ok(person);
        }
        [HttpPost("UpdateMemoryPerson")]
        public async Task<ActionResult<MemoryPersons>> UpdateMemoryPerson([FromBody] MemoryPersons person)
        {
            await Task.Yield();
            // Try to get the list from cache
            var persons = _cache.Get<List<MemoryPersons>>(CacheKey);

            // If list does not exist, create it
            if (persons == null)
            {
                persons = new List<MemoryPersons>();
            }
            var existing = persons.FirstOrDefault(x => x.Id == person.Id);
            if (existing != null)
            {
                // Replace the existing person
                var index = persons.IndexOf(existing);
                person.password = HashCalculation.computeSha1Hash(person.password);
                persons[index] = person;
            }else
            {
                // Add to list
                person.password = HashCalculation.computeSha1Hash(person.password);
                persons.Add(person);
            }
            // Save back into memory
            _cache.Set(CacheKey, persons);

            // Return created person
            return Ok(person);
        }
        [HttpDelete("DeleteMemoryPerson")]
        [HttpDelete("DeleteMemoryPerson/{id}")]
        public async Task<ActionResult> DeletePerson(int? id)
        {
            await Task.Yield();
            // if ID was provided instead of id
            if (id == null && Request.Query.ContainsKey("ID"))
                id = int.Parse(Request.Query["ID"]);

            if (id == null)
                return BadRequest("id is required.");

            var persons = _cache.Get<List<MemoryPersons>>(CacheKey);
            if (persons == null)
                return NotFound("No Persons Stored.");

            var person = persons.FirstOrDefault(x => x.Id == id);
            if (person == null)
                return NotFound("Person not found.");

            persons.Remove(person);
            _cache.Set(CacheKey, persons);

            return Ok("Deleted");
        }
    }
}
