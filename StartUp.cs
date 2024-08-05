
using RPG.Data;
using RPG.Menu;
using RPG.Models.CharacterInfo;
using RPG.Models.Monster;
using RPG.Models.Player;
using RPG.Models.Races;

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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (currentScreen != Screen.Exit)
            {
                switch (currentScreen)
                {
                    case Screen.MainMenu:
                        ShowMainMenu();
                        break;
                    case Screen.CharacterSelect:
                        ShowCharacterSelect();
                        break;
                    case Screen.InGame:
                        PlayGame();
                        break;
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play.");
            Console.ReadKey();
            currentScreen = Screen.CharacterSelect;
        }

        static void ShowCharacterSelect()
        {
            Console.Clear();
            Console.WriteLine("Choose character type:");
            Console.WriteLine("Options:");
            Console.WriteLine("1) Warrior");
            Console.WriteLine("2) Archer");
            Console.WriteLine("3) Mage");
            Console.Write("Your pick: ");

            bool validChoice = false;
            while (!validChoice)
            {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        player = new Warrior();
                        validChoice = true;
                        break;
                    case "2":
                        player = new Archer();
                        validChoice = true;
                        break;
                    case "3":
                        player = new Mage();
                        validChoice = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                        break;
                }
            }

            AllocateBonusPoints();
            SavePlayerToDatabase();
            currentScreen = Screen.InGame;
        }

        static void AllocateBonusPoints()
        {
            Console.WriteLine("Would you like to buff up your stats before starting? (Limit: 3 points total)");
            Console.Write("Response (Y/N): ");
            string response = Console.ReadLine().ToUpper();

            while (response != "Y" && response != "N")
            {
                Console.WriteLine("Please make your choice with Y or N!");
                Console.Write("Response (Y/N): ");
                response = Console.ReadLine().ToUpper();
            }

            if (response == "Y")
            {
                int remainingPoints = 3;

                while (remainingPoints > 0)
                {
                    Console.WriteLine($"Remaining Points: {remainingPoints}");
                    Console.Write("Add to Strength: ");
                    int strengthPoints = int.Parse(Console.ReadLine());
                    remainingPoints -= strengthPoints;
                    if (remainingPoints < 0)
                    {
                        strengthPoints = 0;
                        remainingPoints = 3;
                        Console.WriteLine("Too many points allocated. Try again.");
                    }
                    else if (remainingPoints <= 3 && remainingPoints > 0)
                    {
                        player.Strength += strengthPoints;
                        player.Setup();
                    }
                    else
                    {
                        player.Strength += strengthPoints;
                        player.Setup();
                        continue;
                    }
                    Console.WriteLine($"Remaining Points: {remainingPoints}");
                    Console.Write("Add to Agility: ");
                    int agilityPoints = int.Parse(Console.ReadLine());
                    remainingPoints -= agilityPoints;
                    Console.WriteLine($"Remaining Points: {remainingPoints}");
                    if (remainingPoints < 0)
                    {
                        agilityPoints = 0;
                        remainingPoints = 3;
                        Console.WriteLine("Too many points allocated. Try again.");
                    }
                    else if (remainingPoints <= 3 && remainingPoints > 0)
                    {
                        player.Agility += agilityPoints;
                        player.Setup();
                    }
                    else
                    {
                        player.Strength += strengthPoints;
                        player.Setup();
                        continue;
                    }
                    Console.Write("Add to Intelligence: ");
                    int intelligencePoints = int.Parse(Console.ReadLine());
                    remainingPoints -= intelligencePoints;
                    if (remainingPoints < 0)
                    {
                        intelligencePoints = 0;
                        remainingPoints = 3;
                        Console.WriteLine("Too many points allocated. Try again.");
                    }
                    else if (remainingPoints <= 3 && remainingPoints >= 0)
                    {
                        player.Intelligence += intelligencePoints;
                        player.Setup();
                        continue;
                    }

                }
            }
        }

        static void SavePlayerToDatabase()
        {
            using (var context = new GameContext())
            {
                context.Players.Add(new Player
                {
                    Strength = player.Strength,
                    Agility = player.Agility,
                    Intelligence = player.Intelligence,
                    CreatedAt = DateTime.Now
                });
                context.SaveChanges();
            }
        }

        static void PlayGame()
        {
            int playerX = 1;
            int playerY = 8;

            while (true)
            {
                Console.Clear();
                MoveMonsters(ref playerX, ref playerY);
                Console.WriteLine($"Health: {player.Health} Mana: {player.Mana}");
                DrawGameField(playerX, playerY);

                Console.WriteLine("Choose action:");
                Console.WriteLine("1) Move");
                Console.WriteLine("2) Attack");
                string action = Console.ReadLine();
                while (action != "1" && action != "2")
                {
                    Console.WriteLine("Incorect action! Please input action from the list!");
                    action = Console.ReadLine();
                }

                if (action == "2")
                {
                    PerformAttack(playerX, playerY);
                }
                else if (action == "1")
                {
                    string direction = "";
                    while (direction == "")
                    {
                        Console.WriteLine("Please select direction (WASD/QEZC):");
                        direction = Console.ReadLine().ToUpper();
                    }
                    MovePlayer(direction, ref playerX, ref playerY);
                    SpawnMonster();

                }


                //if (player.Health <= 0)
                //{
                //    Console.WriteLine("You have been defeated. Game over.");
                //    Console.ReadKey();
                //    currentScreen = Screen.Exit;
                //    break;
                //}
            }
        }

        static void DrawGameField(int playerX, int playerY)
        {
            char[,] field = new char[10, 10];

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    field[y, x] = '▒';
                }
            }

            field[playerY, playerX] = player.Symbol;

            foreach (var monster in monsters)
            {
                if (monster.X == playerX && monster.Y == playerY)
                {
                    monster.X = random.Next(0, 10);
                    monster.Y = random.Next(0, 10);
                    field[monster.Y, monster.X] = monster.Symbol;
                    continue;
                }
                else
                {
                    field[monster.Y, monster.X] = monster.Symbol;
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

        static void MovePlayer(string action, ref int playerX, ref int playerY)
        {
            switch (action)
            {
                case "W":
                    if (playerY > 0) playerY--;
                    break;
                case "S":
                    if (playerY < 9) playerY++;
                    break;
                case "A":
                    if (playerX > 0) playerX--;
                    break;
                case "D":
                    if (playerX < 9) playerX++;
                    break;
                case "Q":
                    if (playerX > 0 && playerY > 0) { playerX--; playerY--; }
                    break;
                case "E":
                    if (playerX < 9 && playerY > 0) { playerX++; playerY--; }
                    break;
                case "Z":
                    if (playerX > 0 && playerY < 9) { playerX--; playerY++; }
                    break;
                case "C":
                    if (playerX < 9 && playerY < 9) { playerX++; playerY++; }
                    break;
                default:
                    Console.WriteLine("Incorrect direction! Please select available direction!");
                    action = Console.ReadLine().ToUpper();
                    MovePlayer(action, ref playerX, ref playerY);
                    break;
            }
        }

        static void SpawnMonster()
        {

            monsters.Add(new Monster());

        }

        static void MoveMonsters(ref int playerX, ref int playerY)
        {
            foreach (var monster in monsters)
            {
                bool xDifference = IsDifferenceExactlyOne(monster.X, playerX);
                bool yDifference = IsDifferenceExactlyOne(monster.Y, playerY);
                int currentXDiff = Math.Abs(monster.X - playerX);
                int currentYDiff = Math.Abs(monster.Y - playerY);
                if (xDifference && yDifference || currentXDiff <= 1 && currentYDiff <= 1)
                {
                    player.Health -= monster.Damage;
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("You have been defeated. Game over.");
                        Console.ReadKey();
                        currentScreen = Screen.Exit;
                        break;
                    }
                }
                if (monster.X < playerX - 1) monster.X++;
                else if (monster.X > playerX + 1) monster.X--;


                if (monster.Y < playerY) monster.Y++;
                else if (monster.Y > playerY) monster.Y--;

                if (monster.X == playerX && monster.Y == playerY)
                {
                    monster.X--;
                }

                //if (monster.X == playerX && monster.Y == playerY)
                //{
                //    player.Health -= monster.Damage;
                //}
            }
        }
        static void PerformAttack(int playerX, int playerY)
        {
            var targets = monsters.Where(m => Math.Abs(m.X - playerX) <= player.Range && Math.Abs(m.Y - playerY) <= player.Range).ToList();

            if (targets.Any())
            {


                while (true)
                {

                    Console.WriteLine("Available targets:");
                    for (int i = 0; i < targets.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}) Monster at ({targets[i].X + 1},{targets[i].Y + 1}) with {targets[i].Health} health");
                    }
                    Console.Write("Select target: ");
                    try
                    {
                        int choice = int.Parse(Console.ReadLine()) - 1;
                        if (choice < 0 || choice > targets.Count)
                        {
                            Console.WriteLine("Invalid choice, please choose a correct option for monster to be attacked!");
                            continue;

                        }
                        targets[choice].Health -= player.Damage;

                        if (targets[choice].Health <= 0)
                        {
                            monsters.Remove(targets[choice]);
                        }
                        break;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Invalid choice. The choice must be an integer!");
                    }

                }

            }
            else
            {
                Console.WriteLine("No available targets in your range.");
                Console.WriteLine("Please press a key to continue.");
                Console.ReadKey();
            }
        }
        static bool IsDifferenceExactlyOne(int x, int y)
        {
            return Math.Abs(x - y) == 1;
        }
    }
}
