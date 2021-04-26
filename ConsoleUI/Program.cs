using System;
using BattleShipLiteLibrary;
using BattleShipLiteLibrary.Models;

namespace BattleShipLite
{
    public class Program
    {
        static void Main()
        {
            WelcomeMessage();

            var player1 = CreatePlayer("Player 1");
            var player2 = CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            do
            {
                // Run through the steps of game
                    // Display grid from player 1 on gridmodel
                    // Ask player 1 for shot 
                    //determine if shot is valid
                    // deteming shot results
                    // Determine if game is over
            } while (winner is null);


            Console.ReadLine();
        }

        private static string GetUsersName()
        {
            Console.WriteLine("Please enter your username:");
            var output = Console.ReadLine();
            return output;
        }

        private static bool GridSpotValid(string spotLetter, int spotNumber)
        {
            // Use for placing ship and shot placement

            return true;
        }

        private static void PrintGrid()
        {
            // Come back to and add grid as argument for print

        }

        private static string GenerateGrid()
        {
            // Decide on logic for generating

            return "";
        }

        private static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            // instantiate user and ask for their name
            var output = new PlayerInfoModel {UsersName = GetUsersName()};

            Console.WriteLine($"Player information for { playerTitle }");

            // Load up shot grid
            GameLogic.InitializeGrid(output);

            // Ask user for 5 ship placements
            PlaceShips(output);

            // Clear
            Console.Clear();

            return output;
        }

        private static void FireShot(string spotLetter, int spotNumber)
        {
            if (!GridSpotValid(spotLetter, spotNumber))
            {
                throw new ArgumentOutOfRangeException("spotNumber", "Argument not in range of the grid!");
            }
            
        }

        private static void DisplayScore()
        {
            // Setup to display score 
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("Created by Sean O'Donnell\n");
        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            var ShipCount = model.ShipLocations.Count;
            do
            {
                Console.WriteLine($"Where do you want to place ship #{ShipCount + 1}");
                var location = Console.ReadLine();

                bool isValidLocations = GameLogic.PlaceShip(model, location);

                if (!isValidLocations)
                {
                    Console.WriteLine("That was not a valid location. Please try again.");
                }

            } while (ShipCount < 5);
        }
    }
}
