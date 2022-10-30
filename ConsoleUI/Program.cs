using MyBattleLite;
using MyBattleLite.Models;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Welcome User
            ConsoleMethods.WelcomeMessage();

            PlayerInfoModel activePlayer = ConsoleMethods.CreatePlayer("player 1");
            PlayerInfoModel opponent = ConsoleMethods.CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            do
            {
                ConsoleMethods.AskPlayerToShoot(activePlayer, opponent);

                bool gameOver = GameLogic.IsGameOver(opponent);

                if (gameOver == false)
                {
                    //swap player positions
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    winner = activePlayer;
                }

            }while (winner == null);
            
            // identify winner
            ConsoleMethods.IdentifyWinner(winner);



            Console.ReadLine();
        }
    }
}