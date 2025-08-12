namespace LightboardApi.Models
{
    public class Light
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; } = "#ffffff";
        public int Intensity { get; set; } = 100;
    }
}
