using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Level.LevelDTO;
using UnityEngine;

public interface IDMILevelGenerator
{
    IEnumerable<RoomMetaData> generate(int seed);
}
