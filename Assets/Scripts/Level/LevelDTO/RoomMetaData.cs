using Assets.Scripts.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level.LevelDTO
{
    public class RoomMetaData
    {
        public IntVector2 position;
        public IntVector2 size;
        public IEnumerable<IntVector2> doorLocations;
        public bool hasExit;
        public bool hasStart;
        public int enemyCount = 1; //TODO: populate it in levelGenerator
        public int chestCount = 1; //TODO: populate it in levelGenerator
    }
}
