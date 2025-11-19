using Microsoft.AspNetCore.Mvc;
using WebAPIService.Data;
using WebAPIService.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/person
        [HttpGet("GetAllPersons")]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        [HttpPost("UpdatePerson")]
        public async Task<ActionResult<IEnumerable<Person>>> UpdatePerson([FromBody] Person model)
        {
            if (model == null)
                return BadRequest("Invalid data.");

            // Try to find existing
            var existingPerson = await _context.Persons
                .FirstOrDefaultAsync(x => x.PersonID == model.PersonID);

            if (existingPerson == null)
            {
                // 👇 Create new
                model.PersonID = 0;
                _context.Persons.Add(model);
                await _context.SaveChangesAsync();
                return Ok(model);  // return the created object
            }

            // 👇 Update existing
            existingPerson.LastName = model.LastName;
            existingPerson.FirstName = model.FirstName;
            existingPerson.Weight = model.Weight;
            existingPerson.Height = model.Height;
            existingPerson.Age = model.Age;
            existingPerson.Email = model.Email;

            await _context.SaveChangesAsync();

            return Ok(existingPerson);
        }

        [HttpDelete("DeletePerson")]
        public async Task<ActionResult<IEnumerable<Person>>> DeletePerson(int PersonID)
        {
            var person = await _context.Persons.FindAsync(PersonID);

            if (person == null)
                return NotFound("Person not found.");

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }

        [HttpGet("db-test")]
        public async Task<IActionResult> TestDbConnection()
        {
            try
            {
                await _context.Database.CanConnectAsync();
                return Ok("✅ Database connection successful!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Database connection FAILED: {ex.Message}");
            }
        }

        [HttpGet("test-all")]
        public async Task<IActionResult> TestAll()
        {
            try
            {
                var persons = await _context.Persons.FromSqlRaw("SELECT * FROM dbo.Person").ToListAsync();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
