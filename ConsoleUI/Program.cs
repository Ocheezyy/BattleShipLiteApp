using System;
using BattleShipLiteLibrary;
using BattleShipLiteLibrary.Models;

namespace BattleShipLite
{
    public class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

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

        private static PlayerInfoModel CreatePlayer()
        {
            var output = new PlayerInfoModel {UsersName = GetUsersName()};
            GameLogic.InitializeGrid(output);


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
    }
}
