using System;
using Assets.Scripts.Level.LevelDTO;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

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
            for (int m = 0; m < 3; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    try
                    {
                        neightbours[m, n] = metaTiles[m + central_m - 1, n + central_n - 1];
                    }
                    catch (IndexOutOfRangeException)
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
            switch (neightbours[1, 1])
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
            return assignTile(neightbours, wallTiles);
        }

        private TileEnum assignDoorTile(MetaTileEnum[,] neightbours)
        {
            return assignTile(neightbours, doorTiles);
        }

        private TileEnum assignTile(MetaTileEnum[,] neightbours, IDictionary<SolidOrSpace[,], TileEnum> mapping)
        {
            SolidOrSpace[,] solidOrSpace = toSolidOrSpace(neightbours);
            foreach (KeyValuePair<SolidOrSpace[,], TileEnum> entry in mapping)
                if (SolidOrSpaceHelper.IsCompatibileWith(entry.Key, solidOrSpace))
                    return entry.Value;
            Debug.LogWarning("Could not find suitable mapping for neightbours such as\n" 
                + SolidOrSpaceHelper.ToString(solidOrSpace)
                + "Used default WALL5 instead.");
            return TileEnum.WALL5;
        }

        private SolidOrSpace[,] toSolidOrSpace(MetaTileEnum[,] neightbours)
        {
            if (neightbours.GetLength(0) != 3 || neightbours.GetLength(1) != 3)
                throw new ArgumentOutOfRangeException("Neightbour array must be 3x3");
            SolidOrSpace[,] solidOrSpace = new SolidOrSpace[3, 3];
            for (int m = 0; m < 3; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    switch (neightbours[m, n])
                    {
                        case MetaTileEnum.FLOOR_1:
                        case MetaTileEnum.FLOOR_2:
                        case MetaTileEnum.FLOOR_3:
                            solidOrSpace[m, n] = SolidOrSpace.SPACE;
                            break;
                        case MetaTileEnum.DOOR:
                        case MetaTileEnum.WALL:
                            solidOrSpace[m, n] = SolidOrSpace.SOLID;
                            break;
                    }
                }
            }
            return solidOrSpace;
        }

        //static map; says what tile to use given neightbouring tiles
        public static IDictionary<SolidOrSpace[,], TileEnum> wallTiles =
            new Dictionary<SolidOrSpace[,], TileEnum>
            {
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SPACE, SolidOrSpace.SOLID, SolidOrSpace.SOLID } },
                TileEnum.WALL1
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.ANY  , SolidOrSpace.SPACE, SolidOrSpace.SPACE },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SPACE },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.ANY   } },
                TileEnum.WALL1X
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.ANY  , SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SPACE, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.ANY  , SolidOrSpace.SOLID, SolidOrSpace.SOLID } },
                TileEnum.WALL2
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SPACE, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID } },
                TileEnum.WALL3
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.ANY   },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SPACE },
                    { SolidOrSpace.ANY  , SolidOrSpace.SPACE, SolidOrSpace.SPACE } },
                TileEnum.WALL3X
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.ANY  , SolidOrSpace.SPACE, SolidOrSpace.ANY   } },
                TileEnum.WALL4
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID } },
                TileEnum.WALL5
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.ANY  , SolidOrSpace.SPACE, SolidOrSpace.ANY   },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID } },
                TileEnum.WALL6
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SPACE } },
                TileEnum.WALL7
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SPACE, SolidOrSpace.SPACE, SolidOrSpace.ANY   },
                    { SolidOrSpace.SPACE, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.ANY  , SolidOrSpace.SOLID, SolidOrSpace.SOLID } },
                TileEnum.WALL7X
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.ANY   },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SPACE },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.ANY   } },
                TileEnum.WALL8
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SPACE },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID } },
                TileEnum.WALL9
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.ANY  , SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SPACE, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SPACE, SolidOrSpace.SPACE, SolidOrSpace.ANY   } },
                TileEnum.WALL9X
                }
            };

        public static IDictionary<SolidOrSpace[,], TileEnum> doorTiles =
            new Dictionary<SolidOrSpace[,], TileEnum>
            {
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.ANY   },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SPACE },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.ANY   } },
                TileEnum.DOOR2
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.ANY  , SolidOrSpace.SPACE, SolidOrSpace.ANY   },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID } },
                TileEnum.DOOR4
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SOLID, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.ANY  , SolidOrSpace.SPACE, SolidOrSpace.ANY   } },
                TileEnum.DOOR6
                },
                {
                new SolidOrSpace[3,3] {
                    { SolidOrSpace.ANY  , SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.SPACE, SolidOrSpace.SOLID, SolidOrSpace.SOLID },
                    { SolidOrSpace.ANY  , SolidOrSpace.SOLID, SolidOrSpace.SOLID } },
                TileEnum.DOOR8
                }
            };

        internal enum SolidOrSpace
        {
            ANY,
            SOLID,
            SPACE
        };

        //apparently you cannot override methods inside enums, so I used a helper
        internal static class SolidOrSpaceHelper
        {
            public static bool IsCompatibileWith(SolidOrSpace obj1, SolidOrSpace obj2)
            {
                if ((obj1.Equals(SolidOrSpace.ANY)) || (obj2.Equals(SolidOrSpace.ANY)))
                    return true;
                return obj1.Equals(obj2);
            }

            public static bool IsCompatibileWith(SolidOrSpace[,] arr1, SolidOrSpace[,] arr2)
            {
                if ((arr1.GetLength(0) != arr2.GetLength(0)) || (arr1.GetLength(1) != arr2.GetLength(1)))
                    return false;
                for (int m = 0; m < arr1.GetLength(0); m++)
                    for (int n = 0; n < arr1.GetLength(1); n++)
                        if (!SolidOrSpaceHelper.IsCompatibileWith(arr1[m, n], arr2[m, n]))
                            return false;
                return true;
            }

            public static String ToString(SolidOrSpace obj)
            {
                switch (obj)
                {
                    case SolidOrSpace.ANY:
                        return "?";
                    case SolidOrSpace.SOLID:
                        return "X";
                    case SolidOrSpace.SPACE:
                        return "_";
                }
                throw new NotSupportedException("Could not convert SolidOrSpace to string.");
            }

            public static String ToString(SolidOrSpace[,] arr)
            {
                StringBuilder s = new StringBuilder();
                for (int m = 0; m < arr.GetLength(0); m++)
                {
                    for (int n = 0; n < arr.GetLength(1); n++)
                    {
                        s.Append(SolidOrSpaceHelper.ToString(arr[m, n]));
                    }
                    s.Append("\n");
                }
                return s.ToString();
            }
        };
    };
}
