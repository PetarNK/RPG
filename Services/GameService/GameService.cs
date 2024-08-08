using Microsoft.EntityFrameworkCore;
using RPG.Data;
using RPG.Menu;
using RPG.Models.CharacterInfo;
using RPG.Models.Monster;
using RPG.Models.Player;
using RPG.Services.CharacterService;

namespace RPG.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly ICharacterService _characterService;
        private readonly GameContext _gameContext;

        public GameService(ICharacterService characterService, GameContext gameContext)
        {
            _characterService = characterService;
            _gameContext = gameContext;
        }
        public void RunGame()
        {
             Screen currentScreen = Screen.MainMenu;
             Character player = null;
             List<Monster> monsters = new List<Monster>();
             Random random = new Random();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (currentScreen != Screen.Exit)
            {
                switch (currentScreen)
                {
                    case Screen.MainMenu:
                        ShowMainMenu(ref currentScreen);
                        break;
                    case Screen.CharacterSelect:
                        player = _characterService.CharacterSelect();
                        _characterService.AllocateBonusPoints(player);
                        currentScreen = Screen.InGame;
                        break;
                    case Screen.InGame:
                        PlayGame(ref currentScreen, player, monsters,random);
                        break;
                }
            }
        }
        private void ShowMainMenu(ref Screen currentScreen)
        {
            Console.Clear();
            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play.");
            Console.ReadKey();
            currentScreen = Screen.CharacterSelect;
        }
        
        private void SavePlayerToDatabase(Character player)
        {
            try
            {
                _gameContext.Database.EnsureCreated();
                {
                    _gameContext.Players.Add(new Player
                    {
                        Strength = player.Strength,
                        Agility = player.Agility,
                        Intelligence = player.Intelligence,
                        CreatedAt = DateTime.Now,
                        HighScore = player.HighScore
                    });
                    _gameContext.SaveChanges();
                }
            } catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
        private void PlayGame(ref Screen currentScreen, Character player, List<Monster> monsters, Random random)
        {

            int playerX = 1;
            int playerY = 8;

            while (true)
            {

                Console.Clear();
                currentScreen = MoveMonsters(ref playerX, ref playerY, monsters, player, currentScreen);
                if (currentScreen == Screen.Exit)
                {
                    return;
                }
                Console.WriteLine($"Health: {player.Health} Mana: {player.Mana}");
                DrawGameField(playerX, playerY,player,monsters,random);

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
                    PerformAttack(playerX, playerY,monsters,player);
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
                    SpawnMonster(monsters);

                }

            }
        }
        private void DrawGameField(int playerX, int playerY, Character player, IList<Monster> monsters, Random random)
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

        private void MovePlayer(string action, ref int playerX, ref int playerY)
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

        private void SpawnMonster(IList<Monster> monsters)
        {

            monsters.Add(new Monster());

        }

        private Screen MoveMonsters(ref int playerX, ref int playerY, IList<Monster> monsters, Character player, Screen currentScreen)
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
                        SavePlayerToDatabase(player);
                        Console.Clear();
                        Console.WriteLine("You have been defeated. Game over.");
                        if (player.HighScore > 1 || player.HighScore == 0)
                        {
                            Console.WriteLine($"Your highscore is: {player.HighScore} monsters defeated");

                        }
                        else 
                        { 
                            Console.WriteLine($"Your highscore is: {player.HighScore} monster defeated"); 
                        }
                        currentScreen = Screen.Exit;
                        Console.ReadKey();
                        return currentScreen;
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

            }
            return currentScreen;
        }
        private void PerformAttack(int playerX, int playerY, IList<Monster> monsters, Character player)
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
                        if (choice < 0 || choice >= targets.Count)
                        {
                            Console.WriteLine("Invalid choice, please choose a correct option for monster to be attacked!");
                            continue;

                        }
                        targets[choice].Health -= player.Damage;

                        if (targets[choice].Health <= 0)
                        {
                            monsters.Remove(targets[choice]);
                            player.HighScore++;
                        }
                        break;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Invalid choice. The choice must be an integer!", e);
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
        private bool IsDifferenceExactlyOne(int x, int y)
        {
            return Math.Abs(x - y) == 1;
        }
    }
}
