using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Level.LevelDTO;
using UnityEngine;
using Assets.Scripts.Util;

public interface ILevelGenerator
{
    //returns data about all rooms on the map given seed and level boundaries
    IEnumerable<RoomMetaData> generate(int seed, ref IntVector2 levelSize);
}
