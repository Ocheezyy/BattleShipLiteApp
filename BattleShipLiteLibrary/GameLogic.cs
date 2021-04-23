using System.Collections.Generic;
using BattleShipLiteLibrary.Models;

namespace BattleShipLiteLibrary
{
    public static class GameLogic
    {
        public static void InitializeGrid(PlayerInfoModel model)
        {
            var letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            var numbers = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            foreach (var letter in letters)
            {
                foreach (var number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }
            }
        }

        private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
        {
            var spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShotGrid.Add(spot);
        }
    }
}
