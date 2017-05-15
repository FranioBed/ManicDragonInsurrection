using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.Level.TilesTranslator;
using Assets.Scripts.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTilesTranslator : ITilesTranslator
{
    public TileEnum[,] translate(MetaTileEnum[,] metaTiles)
    {
        //simplest implementation. uses only two tile types
        TileEnum[,] finalTiles = new TileEnum[metaTiles.GetLength(0), metaTiles.GetLength(1)];

        for (int m = 0; m < metaTiles.GetLength(0); m++)
        {
            for (int n = 0; n < metaTiles.GetLength(1); n++)
            {
                switch (metaTiles[m, n]) { 
                    case MetaTileEnum.FLOOR_1:
                    case MetaTileEnum.FLOOR_2:
                    case MetaTileEnum.FLOOR_3:
                    case MetaTileEnum.DOOR:
                        finalTiles[m, n] = TileEnum.FLOOR1;
                        break;
                    default:
                        finalTiles[m, n] = TileEnum.WALL5;
                        break;
                }
            }
        }
        return finalTiles;
    }
}
