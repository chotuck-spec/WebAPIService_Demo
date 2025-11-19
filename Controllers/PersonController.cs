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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
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
