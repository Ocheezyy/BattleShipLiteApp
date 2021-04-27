using System;
using System.Globalization;
using System.Threading;
using BattleShipLiteLibrary;
using BattleShipLiteLibrary.Models;

namespace BattleShipLite
{
    public class Program
    {
        static void Main()
        {
            WelcomeMessage();

            var activePlayer = CreatePlayer("Player 1");
            var opponent = CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            do
            {
                Console.Clear();
                DisplayShotGrid(activePlayer);

                RecordPlayerShot(activePlayer, opponent);

                var doesGameContinue = GameLogic.PlayerStillActive(opponent);
                if (doesGameContinue)
                {
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                { winner = activePlayer; }

            } while (winner is null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.Clear();
            Console.WriteLine($"Congratulations to {winner.UsersName} for winning!");
            Console.WriteLine($"{ char.ToUpper(winner.UsersName[0]) + winner.UsersName[1..]} took { GameLogic.GetShotCount(winner) } shots, { (5.0 / GameLogic.GetShotCount(winner)).ToString("P1", CultureInfo.DefaultThreadCurrentCulture) } accuracy");
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            var isValidShot = false;
            var row = "";
            var column = 0;
            do
            {
                var shot = AskForShot(activePlayer.UsersName);
                if (shot.Length != 2)
                { continue; }
                (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                isValidShot = GameLogic.ValidateShot(activePlayer, row, column);

                if (!isValidShot)
                {
                    Console.WriteLine("Invalid shot location. Please try again.");
                }
            } while (!isValidShot);

            var isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

            // Record results
            var output = GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
            if (!string.IsNullOrEmpty(output)){
                Console.WriteLine(output);
                Console.WriteLine("Press enter to proceed...");
                Console.ReadLine();
            }
        }

        private static string AskForShot(string username)
        {
            Console.Write($"\nPlease enter your shot selection, {username}: ");
            var output = Console.ReadLine();
            return output;
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            var currentRow = activePlayer.ShotGrid[0].SpotLetter;
            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }

                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                {
                    Console.Write(" ? ");
                }
            }
        }

        private static string GetUsersName()
        {
            Console.WriteLine("Please enter your username:");
            var output = Console.ReadLine();
            return output;
        }

        private static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            // instantiate user and ask for their name
            Console.WriteLine($"Player information for { playerTitle }");
            var output = new PlayerInfoModel {UsersName = GetUsersName()};

            // Load up shot grid
            GameLogic.InitializeGrid(output);

            // Ask user for 5 ship placements
            PlaceShips(output);

            // Clear
            Console.Clear();

            return output;
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("Created by Sean O'Donnell\n");
        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            var shipCount = model.ShipLocations.Count;
            do
            {
                Console.WriteLine($"Where do you want to place ship #{shipCount + 1}");
                var location = Console.ReadLine();

                bool isValidLocations = GameLogic.PlaceShip(model, location);

                if (!isValidLocations)
                {
                    Console.WriteLine("That was not a valid location. Please try again.");
                }

                shipCount = model.ShipLocations.Count;

            } while (shipCount < 5);
        }
    }
}
