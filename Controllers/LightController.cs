// using System.Text.Json; //JsonSerializer
using LightboardApi.Data; //LightContext.cs
using LightboardApi.Models; //Light.cs
using Microsoft.AspNetCore.Mvc; //gives MVC/Web API attributes ([HttpGet], [HttpPost], ControllerBase...)
using Microsoft.EntityFrameworkCore;

namespace LightboardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //define URL here: api/light(without Controller)/save or load
    public class LightController : ControllerBase //no view engine as in Controller
    {
        private readonly LightContext _context;

        public LightController(LightContext context)
        {
            _context = context;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveScene([FromBody] Scene scene) //IActionResult for return just message
        {
            // _context.Lights.RemoveRange(_context.Lights); //replace the previous lights

            _context.Scenes.Add(scene);
            await _context.SaveChangesAsync();

            return Ok("Scene saved to SQLite in-memory DB.");
        }

        [HttpGet("load/{id}")]
        public async Task<ActionResult<Scene>> LoadScene(int id) //ActionResult for return more
        {
            var scene = await _context
                .Scenes.Include(s => s.Lights) // Include the related lights
                .FirstOrDefaultAsync(s => s.Id == id);

            if (scene == null)
            {
                return NotFound($"Scene with id{id} not found.");
            }

            return Ok(scene);
        }

        [HttpGet("loadall")]
        public async Task<ActionResult<Scene>> GetAllScenes()
        {
            var allScenes = await _context.Scenes.Include(s => s.Lights).ToListAsync();

            if (allScenes.Count == 0)
                return NotFound("No scene saved in DB.");

            return Ok(allScenes);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeteleScene(int id)
        {
            var scene = await _context
                .Scenes.Include(s => s.Lights)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (scene == null)
            {
                return NotFound($"Scene with id{id} not found.");
            }

            _context.Scenes.Remove(scene);
            await _context.SaveChangesAsync();

            return Ok($"Scene {id} deleted.");
        }

        [HttpPatch("edit/{id}")]
        public async Task<IActionResult> EditScene(int id, [FromBody] List<Light> updatedLights)
        {
            var scene = await _context
                .Scenes.Include(s => s.Lights)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (scene == null)
            {
                return NotFound($"Scene with id{id} not found.");
            }

            _context.Lights.RemoveRange(scene.Lights);

            foreach (var light in updatedLights)
            {
                light.SceneId = scene.Id;
            }

            scene.Lights = updatedLights;
            await _context.SaveChangesAsync();

            return Ok($"Scene {id} updated successfully.");
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
