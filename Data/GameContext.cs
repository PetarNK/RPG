using Microsoft.EntityFrameworkCore;
using RPG.Models.CharacterInfo;
using RPG.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
