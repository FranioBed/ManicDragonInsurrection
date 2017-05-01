using System;
using Assets.Scripts.Level.LevelDTO;
using UnityEngine;

namespace Assets.Scripts.Level.TilesTranslator
{
    class DMIFancyBoundariesTilesTranslator : IDMITilesTranslator
    {
        public TileEnum[,] translate(MetaTileEnum[,] metaTiles)
        {
            //simplest implementation. uses only two tile types
            TileEnum[,] finalTiles = new TileEnum[metaTiles.GetLength(0), metaTiles.GetLength(1)];

            for (int m = 0; m < metaTiles.GetLength(0); m++)
                for (int n = 0; n < metaTiles.GetLength(1); n++)
                {
                    MetaTileEnum[,] neightbours = getNeighbours(ref metaTiles, m, n);
                    finalTiles[m, n] = assignTileBasedOnNeighbours(neightbours);
                }
            return finalTiles;
        }

        private MetaTileEnum[,] getNeighbours(ref MetaTileEnum[,] metaTiles, int central_m, int central_n)
        {
            //creates 3x3 grid with processed tile in center. tiles outside of the map are treated as walls
            MetaTileEnum[,] neightbours = new MetaTileEnum[3, 3];
            for (int m = 0; m < 3 + 1; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    try
                    {
                        neightbours[m, n] = metaTiles[m + central_m - 1, n + central_n - 1];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        neightbours[m, n] = MetaTileEnum.WALL;
                    }
                }
            }
            return neightbours;
        }

        private TileEnum assignTileBasedOnNeighbours(MetaTileEnum[,] neightbours)
        {
            if (neightbours.GetLength(0) != 3 || neightbours.GetLength(1) != 3)
                throw new ArgumentOutOfRangeException("Neightbour array must be 3x3");
            //based on central piece:
            switch (neightbours[1,1])
            {
                case MetaTileEnum.FLOOR_1:
                    return TileEnum.FLOOR1;
                case MetaTileEnum.FLOOR_2:
                    return TileEnum.FLOOR2;
                case MetaTileEnum.FLOOR_3:
                    return TileEnum.FLOOR3;
                case MetaTileEnum.WALL:
                    return assignWallTile(neightbours);
                case MetaTileEnum.DOOR:
                    return assignDoorTile(neightbours);
                default:
                    Debug.LogError("Ecountered unknown MetaTile while creating level layout! Used WALL5 placeholder.");
                    return TileEnum.WALL5;
            }
        }

        private TileEnum assignWallTile(MetaTileEnum[,] neightbours)
        {
            throw new NotImplementedException();
        }

        private TileEnum assignDoorTile(MetaTileEnum[,] neightbours)
        {
            throw new NotImplementedException();
        }
    }
}
