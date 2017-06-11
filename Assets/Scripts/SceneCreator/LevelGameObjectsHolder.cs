using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.SceneCreator
{
    public class LevelGameObjectsHolder
    {
        public IList<GameObject> tiles = new List<GameObject>();
        public IList<GameObject> items = new List<GameObject>();
        public IList<GameObject> enemies = new List<GameObject>();
        public Marker startPos;
    }
}
