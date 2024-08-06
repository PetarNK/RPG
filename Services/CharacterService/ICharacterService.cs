using RPG.Models.CharacterInfo;

namespace RPG.Services.CharacterService
{
    public interface ICharacterService
    {
        Character CharacterSelect();
        void AllocateBonusPoints(Character character);
    }
}
