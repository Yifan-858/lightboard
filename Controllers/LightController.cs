// using System.Text.Json; //JsonSerializer
using LightboardApi.Data; //LightContext.cs
using LightboardApi.Models; //Light.cs
using Microsoft.AspNetCore.Mvc; //gives MVC/Web API attributes ([HttpGet], [HttpPost], ControllerBase...)
using Microsoft.EntityFrameworkCore;

namespace LightboardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LightController : ControllerBase //no view engine as in Controller
    {
        private readonly LightContext _context;

        public LightController(LightContext context)
        {
            _context = context;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveScene([FromBody] List<Light> lights)
        {
            _context.Lights.RemoveRange(_context.Lights);

            await _context.Lights.AddRangeAsync(lights);
            await _context.SaveChangesAsync();

            return Ok("Scene saved to SQLite in-memory DB.");
        }

        [HttpGet("load")]
        public async Task<ActionResult<List<Light>>> LoadScene()
        {
            var lights = await _context.Lights.ToListAsync();

            if (lights.Count == 0)
                return NotFound("No scene saved in DB.");

            return Ok(lights);
        }
        // private const string FilePath = "lights.json";

        // [HttpPost("save")]
        // public IActionResult SaveScene([FromBody] List<Light> lights)
        // {
        //     System.IO.File.WriteAllText(FilePath, JsonSerializer.Serialize(lights));
        //     return Ok("Scene saved.");
        // }

        // [HttpGet("load")]
        // public ActionResult<List<Light>> LoadScene()
        // {
        //     if (!System.IO.File.Exists(FilePath))
        //         return NotFound("No scene saved.");

        //     var json = System.IO.File.ReadAllText(FilePath);
        //     var lights = JsonSerializer.Deserialize<List<Light>>(json);
        //     return Ok(lights);
        // }
    }
}
