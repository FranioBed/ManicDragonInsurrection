﻿using System.Collections.Generic;
using Assets.Scripts.Level.LevelDTO;

public interface IIRoomGenerator
{
    //manipulate metaTiles structure to decide what elements are on the level grid
    void generate(int seed, IEnumerable<RoomMetaData> roomList,
        ref MetaTileEnum[,] metaTiles, ref ItemOnTileEnum[,] itemsOnTiles);
}
