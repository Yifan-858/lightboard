using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LightboardApi.Models;


namespace LightboardApi.Controllers 
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class LightController : ControllerBase
    {
        private const string FilePath = "lights.json";

        [HttpPost("save")]
        public IActionResult SaveScene([FromBody] List<Light> lights)
        {
            System.IO.File.WriteAllText(FilePath, JsonSerializer.Serialize(lights));
            return Ok("Scene saved.");
        }

        [HttpGet("load")]
        public ActionResult<List<Light>> LoadScene()
        {
            if (!System.IO.File.Exists(FilePath))
                return NotFound("No scene saved.");

            var json = System.IO.File.ReadAllText(FilePath);
            var lights = JsonSerializer.Deserialize<List<Light>>(json);
            return Ok(lights);
        }
    }
}
