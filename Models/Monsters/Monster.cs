using RPG.Models.CharacterInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Models.Monster
{
    public class Monster : Character
    {
        private static Random random = new Random();
        public int X { get; set; }
        public int Y { get; set; }

        public Monster()
        {
            this.X = random.Next(0, 10);
            this.Y = random.Next(0, 10);
            this.Strength = random.Next(1, 4);
            this.Agility = random.Next(1, 4);
            this.Intelligence = random.Next(1, 4);
            this.Range = 1;
            this.Symbol = '◙';
            Setup();
        }
    }
}
