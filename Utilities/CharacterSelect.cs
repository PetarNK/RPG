using RPG.Data;
using RPG.Menu;
using RPG.Models.CharacterInfo;
using RPG.Models.Player;
using RPG.Models.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Utilities
{
    public class CharacterSelect
    {

        public static void ShowCharacterSelect(Screen currentScreen, Character player)
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

            AllocateBonusPoints(player);
            SavePlayerToDatabase(player);
            currentScreen = Screen.InGame;
        }
        static void AllocateBonusPoints(Character player)
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

        static void SavePlayerToDatabase(Character player)
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
    }
}
