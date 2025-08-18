using System.ComponentModel.DataAnnotations; //needed for [Key]
using System.Text.Json.Serialization; //for storing in json

namespace LightboardApi.Models
{
    public class Light
    {
        [Key] // optional if named Id
        public int Id { get; set; } // EF Core primary key

        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; } = "#ffffff";

        [JsonPropertyName("intensity")]
        public int Intensity { get; set; } = 100;

        [JsonPropertyName("radius")]
        public int Radius { get; set; } = 20;
    }
}
