using RPG.Models.CharacterInfo;

namespace RPG.Models.Races
{
    public class Mage : Character
    {
        public Mage()
        {
            this.Strength = 2;
            this.Agility = 1;
            this.Intelligence = 3;
            this.Range = 3;
            this.Symbol = '*';
            Setup();
        }

    }
}
