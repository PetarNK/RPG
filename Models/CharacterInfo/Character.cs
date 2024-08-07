using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Models.CharacterInfo
{
    public abstract class Character
    {
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Range { get; set; }
        public char Symbol { get; set; }

        public int HighScore { get; set; }

        public virtual void Setup()
        {
            this.Health = this.Strength * 5;
            this.Mana = this.Intelligence * 3;
            this.Damage = this.Agility * 2;
        }
    }
}
