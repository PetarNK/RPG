
using RPG.Utilities;

namespace RPG
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            Greeter.ShowMainMenu();

            Drawer.DrawGameField();
        }
    }
}