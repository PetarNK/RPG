
using RPG.Menu;
using RPG.Models.CharacterInfo;
using RPG.Models.Monster;
using RPG.Utilities;

namespace RPG
{
    public class StartUp
    {
        static Screen currentScreen = Screen.MainMenu;
        static Character player;
        static List<Monster> monsters = new List<Monster>();
        static Random random = new Random();
        static void Main(string[] args)
        {

            Greeter.ShowMainMenu();


            CharacterSelect.ShowCharacterSelect(currentScreen, player);

            Drawer.DrawGameField();
        }
    }
}