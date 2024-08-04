﻿using RPG.Models.CharacterInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Models.Races
{
    public class Warrior : Character
    {
        public Warrior()
        {
            this.Strength = 3;
            this.Agility = 3;
            this.Intelligence = 0;
            this.Range = 1;
            this.Symbol = '@';
            Setup();
        }
    }
}
