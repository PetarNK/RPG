using Microsoft.EntityFrameworkCore;
using RPG.Models.Player;


namespace RPG.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set;}

        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(p => p.Id);
        }


    }
}
