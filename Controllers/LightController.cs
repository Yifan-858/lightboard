using Microsoft.AspNetCore.Mvc; //gives MVC/Web API attributes ([HttpGet], [HttpPost], ControllerBase...)
using System.Text.Json;//JsonSerializer
using LightboardApi.Models;//Light.cs


namespace LightboardApi.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class LightController : ControllerBase //no view engine as in Controller
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
