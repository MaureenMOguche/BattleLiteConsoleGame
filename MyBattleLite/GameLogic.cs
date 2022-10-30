using MyBattleLite.Models;

namespace MyBattleLite
{
    public class GameLogic
    {
        public static (string row, int column) SplitShotToRowAndColumn(string location)
        {
            string row = location[0].ToString().ToUpper();
            int.TryParse(location[1].ToString(), out int column);
            return (row, column);
        }

        public static void StoreShipRecord(PlayerInfoModel player, string row, int column)
        {
            GridSpotModel spot = new GridSpotModel();
            spot.SpotLetter = row;
            spot.SpotNumber = column;
            spot.SpotStatus = GridSpotStatus.Ship;


            player.ShipLocations.Add(spot);
        }

        public static bool IsGameOver(PlayerInfoModel opponent)
        {
            foreach (var spot in opponent.ShipLocations)
            {
                if (spot.SpotStatus != GridSpotStatus.Sunk)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ValidateLocation(PlayerInfoModel player, string row, int column)
        {
            foreach (var spot in player.GridShotLocations)
            {
                if (row == spot.SpotLetter && column == spot.SpotNumber)
                {
                    foreach (var inspot in player.ShipLocations)
                    {
                        if (spot.SpotLetter == inspot.SpotLetter && spot.SpotNumber == inspot.SpotNumber)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        public static void InitializeGrid(PlayerInfoModel player)
        {
            List<string> letters = new(){ "A", "B", "C", "D", "E"};
            List<int> numbers = new(){1, 2, 3, 4, 5};

            foreach (var letter in letters)
            {
                foreach (var number in numbers)
                {
                    AddGridSpot(player, letter, number);
                }
            }
        }

        private static void AddGridSpot(PlayerInfoModel player, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel();
            spot.SpotLetter = letter;
            spot.SpotNumber = number;
            spot.SpotStatus = GridSpotStatus.Empty;


            player.GridShotLocations.Add(spot);
            // player.ShipLocations.Add(spot);
        }

        public static bool ValidateShotLocation(PlayerInfoModel player, string row, int column)
        {
            foreach (var spot in player.GridShotLocations)
            {
                if (spot.SpotLetter == row && spot.SpotNumber == column && spot.SpotStatus == GridSpotStatus.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        public static void StoreShotRecord(PlayerInfoModel player, PlayerInfoModel opponent, string row, int column)
        {
            bool isAHit = DetermineShotResult(opponent, row, column);

            foreach (var spot in player.GridShotLocations)
            {
                if (spot.SpotLetter == row && spot.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        spot.SpotStatus = GridSpotStatus.Hit;
                    }
                    else
                    {
                        spot.SpotStatus = GridSpotStatus.Miss;
                    }
                }
            }

            foreach (var opponentSpot in opponent.ShipLocations)
            {
                if (opponentSpot.SpotLetter == row && opponentSpot.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        opponentSpot.SpotStatus = GridSpotStatus.Sunk;
                    }
                }
            }

            
        }

        private static bool DetermineShotResult(PlayerInfoModel opponent, string row, int column)
        {
            foreach (var shot in opponent.ShipLocations)
            {
                if (shot.SpotLetter == row && shot.SpotNumber == column && shot.SpotStatus == GridSpotStatus.Ship)
                {
                    return true;
                }
            }
            return false;
        }
    }
}