using RPG.Models.CharacterInfo;
using RPG.Models.Races;

namespace RPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        public Character CharacterSelect()
        {
            Console.Clear();
            Console.WriteLine("Choose character type:");
            Console.WriteLine("Options:");
            Console.WriteLine("1) Warrior");
            Console.WriteLine("2) Archer");
            Console.WriteLine("3) Mage");
            Console.Write("Your pick: ");


            while (true)
            {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        return new Warrior();
                    case "2":
                        return new Archer();
                    case "3":
                        return new Mage();
                    default:
                        Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                        break;
                }
            }

        }

        public void AllocateBonusPoints(Character character)
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
                    try
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
                            character.Strength += strengthPoints;
                            character.Setup();
                        }
                        else
                        {
                            character.Strength += strengthPoints;
                            character.Setup();
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
                            character.Agility += agilityPoints;
                            character.Setup();
                        }
                        else
                        {
                            character.Strength += strengthPoints;
                            character.Setup();
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
                            character.Intelligence += intelligencePoints;
                            character.Setup();
                            continue;
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Invalid choice. The choice must be an integer!", ex);
                    }


                }
            }
            else
            {
                character.Setup();

            }
        }

        
    }
}
