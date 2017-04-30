using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level.LevelDTO
{
    public class RoomMetaData
    {
        public Vector2 position;
        public Vector2 size;
        public IEnumerable<Vector2> doorLocations;
        public bool hasExit;
    }
}
