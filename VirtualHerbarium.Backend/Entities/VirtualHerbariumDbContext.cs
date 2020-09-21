using Microsoft.EntityFrameworkCore;

namespace VirtualHerbarium.Backend.Entities
{
    public class VirtualHerbariumDbContext : DbContext
    {
        public VirtualHerbariumDbContext(DbContextOptions<VirtualHerbariumDbContext> options)
            : base(options)
        {
        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<User> Users { get; set; }
    }
}