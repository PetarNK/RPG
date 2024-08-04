using RPG.Models.CharacterInfo;

namespace RPG.Models.Races
{
    public class Archer : Character
    {
        public Archer()
        {
            this.Strength = 2;
            this.Agility = 4;
            this.Intelligence = 0;
            this.Range = 2;
            this.Symbol = '#';
            Setup();
        }
    }
}
