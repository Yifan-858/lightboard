using LightboardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LightboardApi.Data
{
    public class LightContext : DbContext
    {
        public LightContext(DbContextOptions<LightContext> options)
            : base(options) { }

        public DbSet<Light> Lights { get; set; }
    }
}
