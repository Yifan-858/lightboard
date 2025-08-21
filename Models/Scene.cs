using System.ComponentModel.DataAnnotations; //needed for [Key]

namespace LightboardApi.Models
{
    public class Scene
    {
        [Key]
        public int Id { get; set; }
        public List<Light> Lights { get; set; } = new();
    }
}
