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
        public int enemyCount;
        public int chestCount;
    }
}
