
namespace RPG
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            static void ShowMainMenu()
            {
                Console.Clear();
                Console.WriteLine("Welcome!");
                Console.WriteLine("Press any key to play.");
                Console.ReadKey();
            }
            ShowMainMenu();

            static void DrawGameField()
            {
                char[,] field = new char[10, 10];

                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        field[y, x] = '▒';
                    }
                }


                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        Console.Write(field[y, x]);
                    }
                    Console.WriteLine();
                }
            }

            DrawGameField();
        }
    }
}