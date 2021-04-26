using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
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

        public static bool PlayerStillActive(PlayerInfoModel player)
        {
            var isActive = false;

            foreach (var gridSpot in player.ShipLocations)
            {
                if (gridSpot.Status == GridSpotStatus.Ship)
                {
                    isActive = true;
                }
            }
            return isActive;
        }

        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            var output = false;
            var (row, column) = SplitShotIntoRowAndColumn(location);

            var isValidLocation = ValidateGridLocation(model, row, column);
            var isSpotOpen = ValidateShipLocation(model, row, column);

            if (isSpotOpen && isValidLocation)
            {
                model.ShipLocations.Add(new GridSpotModel()
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });
                output = true;
            }

            return output;
        }

        private static bool ValidateShipLocation(PlayerInfoModel model, string row, int column)
        {
            var isValidLocation = true;
            foreach (var ship in model.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = false;
                }
            }

            return isValidLocation;
        }

        private static bool ValidateGridLocation(PlayerInfoModel model, string row, int column)
        {
            var isValidLocation = false;
            foreach (var gridSpot in model.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    isValidLocation = true;
                }
            }

            return isValidLocation;
        }

        public static int GetShotCount(PlayerInfoModel winner)
        {
            var shotQuery =
                from gridSpot in winner.ShotGrid
                where gridSpot.Status != GridSpotStatus.Empty
                select gridSpot;

            return shotQuery.Count();
        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            shot = shot.Trim();
            var shotArr = shot.ToArray();

            if (shotArr.Length != 2)
            {
                throw new ArgumentException("An inserted shot value was invalid", nameof(shot));
            }

            var (row, column ) = (shotArr[0].ToString(), int.Parse(shotArr[1].ToString()));
            
            return (row, column);
        }

        public static bool ValidateShot(PlayerInfoModel player, string row, int column)
        {
            var isValidShot = false;

            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (gridSpot.Status == GridSpotStatus.Empty)
                    {
                        isValidShot = true;
                    }
                }
            }

            return isValidShot;
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, int column)
        {
            var isAHit = false;

            foreach (var ship in opponent.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isAHit = true;
                    ship.Status = GridSpotStatus.Sunk;
                }
            }

            return isAHit;
            
        }

        public static void MarkShotResult(PlayerInfoModel player, string row, int column, bool isAHit)
        {
            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    gridSpot.Status = isAHit ? GridSpotStatus.Hit : GridSpotStatus.Miss;
                }
            }
        }
    }
}
