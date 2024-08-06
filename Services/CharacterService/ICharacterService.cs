using RPG.Models.CharacterInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Services.CharacterService
{
    public interface ICharacterService
    {
        Character CharacterSelect();
        void AllocateBonusPoints(Character character);
    }
}
