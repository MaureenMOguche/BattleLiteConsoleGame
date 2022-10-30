using MyBattleLite.Models;
using MyBattleLite;
using System.Linq;


namespace ConsoleUI
{
    public class ConsoleMethods
    {
        internal static void WelcomeMessage()
        {
            System.Console.WriteLine("Hello! Welcome to Battle Ship Lite!");
            System.Console.WriteLine("Created by Maureen Oguche (2022)");
            System.Console.WriteLine("**************************************\n");
        }

        internal static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel player = new PlayerInfoModel();
            // ask for users name
            player.UsersName = AskForUsersName(playerTitle);
            // load the grid
            GameLogic.InitializeGrid(player);
            // ask for ship placements
            PlaceShips(player);

            return player;
        }

        private static void PlaceShips(PlayerInfoModel player)
        {
            string row = "";
            int column = 0;

            do
            {
                System.Console.WriteLine($"Where do you want to place ship number {player.ShipLocations.Count + 1}");
                string location = Console.ReadLine();
                
                while (location.Length < 2)
                {
                    System.Console.WriteLine("Invalid entry, location must be two letters");
                    location = Console.ReadLine();
                }

                (row, column) = GameLogic.SplitShotToRowAndColumn(location);

                bool isValidLocation = GameLogic.ValidateLocation(player, row, column);

                if (isValidLocation == true)
                {
                    GameLogic.StoreShipRecord(player, row, column);
                }
                else
                {
                    System.Console.WriteLine("Invalid location, try again");
                    System.Console.WriteLine("Location must be one alphabet (A - E) and one number (1 - 5). No spaces.");
                }

            } while (player.ShipLocations.Count < 5);

            Console.Clear(); 
        }

        private static string AskForUsersName(string playerTitle)
        {
            System.Console.WriteLine($"Enter user name for { playerTitle }");
            string output = Console.ReadLine();
            bool isLetter = output.All(output => (output >= 'a' && output <= 'z') || (output >= 'A' && output <= 'Z'));

            while (output.Length < 3 || isLetter == false)
            {
                Console.WriteLine("Invalid Username. Please enter username not less than 3 characters");
                output = Console.ReadLine();
                isLetter = output.All(output => (output >= 'a' && output <= 'z') || (output >= 'A' && output <= 'Z'));
            }

            return output;
        }

        internal static void AskPlayerToShoot(PlayerInfoModel player, PlayerInfoModel opponent)
        {
            System.Console.WriteLine($"Hello {player.UsersName}");
            System.Console.WriteLine("Your current Shotgrid: \n");

            DisplayShotGrid(player);
            System.Console.WriteLine("\n*************************\n");

            System.Console.WriteLine("Now where do you want to place your shot? ");

            bool isValidLocation;
            do
            {
                string location = Console.ReadLine();
                (string row, int column) = GameLogic.SplitShotToRowAndColumn(location);
                
                isValidLocation = GameLogic.ValidateShotLocation(player, row, column);

                if (isValidLocation)
                {
                    GameLogic.StoreShotRecord(player, opponent, row, column);
                    player.ShotCount++;
                }
                else
                {
                    System.Console.WriteLine("Invalid shot, try again ");

                }
            }while (isValidLocation == false);
            
            Console.Clear();
        }

        private static void DisplayShotGrid(PlayerInfoModel player)
        {
            string currentRow = player.GridShotLocations[0].SpotLetter;

            foreach (var spot in player.GridShotLocations)
            {
                if (currentRow != spot.SpotLetter)
                {
                    System.Console.WriteLine();
                    currentRow = spot.SpotLetter;
                }
                if (spot.SpotStatus == GridSpotStatus.Empty)
                {
                    System.Console.Write($" {spot.SpotLetter}{spot.SpotNumber} ");
                }
                else if (spot.SpotStatus == GridSpotStatus.Hit)
                {
                    System.Console.Write(" X ");
                }
                else if (spot.SpotStatus == GridSpotStatus.Miss)
                {
                    System.Console.Write(" O ");
                }
                else
                {
                    System.Console.Write(" ? ");
                }
            }
        }

        internal static void IdentifyWinner(PlayerInfoModel winner)
        {
            System.Console.WriteLine("\n****************************\n");
            System.Console.WriteLine("******   GAME OVER   ******");
            System.Console.WriteLine($"Congratulations! {winner.UsersName}");
            System.Console.WriteLine($"{winner.UsersName} won after {winner.ShotCount} shots");
            System.Console.WriteLine("\n******   GAME OVER   ******");
            System.Console.WriteLine("\n****************************");
        }
    }
}