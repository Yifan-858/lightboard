using System.Text.Json.Serialization;

namespace LightboardApi.Models
{
    public class Light
    {
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
