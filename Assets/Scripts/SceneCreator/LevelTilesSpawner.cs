using Assets.Scripts.Level.LevelDTO;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.SceneCreator
{
    //Factory might be a little overkill given amount of prefabs to configure
    public class LevelTilesSpawner
    {
        [Inject]
        DMISettingsInstaller.LevelTiles _levelTiles;

        public static IDictionary<TileEnum, GameObject> tilesMapping = null;

        public List<GameObject> spawnMap(TileEnum[,] tiles)
        {
            if (tilesMapping == null)
                populateMap();
            List<GameObject> tilesList = new List<GameObject>();
            for (int m = 0; m < tiles.GetLength(0); m++)
            {
                for (int n = 0; n < tiles.GetLength(1); n++)
                {
                    GameObject tileInstance = createNewTile(tiles, m, n);
                    tilesList.Add(tileInstance);
                }
            }
            return tilesList;
        }

        private GameObject createNewTile(TileEnum[,] tiles, int m, int n)
        {
            GameObject prefab = tilesMapping[tiles[m, n]];
            GameObject tileInstance = GameObject.Instantiate(prefab);
            //FIXME: Use GameObject reference instead of reflection
            tileInstance.transform.parent = GameObject.Find("TilesContainer").transform;
            tileInstance.transform.Translate(new Vector3(_levelTiles.tileSpan * m, _levelTiles.tileSpan * -n));
            return tileInstance;
        }

        private void populateMap()
        {
             tilesMapping = new Dictionary<TileEnum, GameObject>() {
                { TileEnum.FLOOR1, _levelTiles.floor1 },
                { TileEnum.FLOOR2, _levelTiles.floor2 },
                { TileEnum.FLOOR3, _levelTiles.floor3 },
                { TileEnum.WALL1 , _levelTiles.wall1  },
                { TileEnum.WALL1X, _levelTiles.wall1x },
                { TileEnum.WALL2 , _levelTiles.wall2  },
                { TileEnum.WALL3 , _levelTiles.wall3  },
                { TileEnum.WALL3X, _levelTiles.wall3x },
                { TileEnum.WALL4 , _levelTiles.wall4  },
                { TileEnum.WALL5 , _levelTiles.wall5  },
                { TileEnum.WALL6 , _levelTiles.wall6  },
                { TileEnum.WALL7 , _levelTiles.wall7  },
                { TileEnum.WALL7X, _levelTiles.wall7x },
                { TileEnum.WALL8 , _levelTiles.wall8  },
                { TileEnum.WALL9 , _levelTiles.wall9  },
                { TileEnum.WALL9X, _levelTiles.wall9x },
                { TileEnum.DOOR2 , _levelTiles.door2  },
                { TileEnum.DOOR4 , _levelTiles.door4  },
                { TileEnum.DOOR6 , _levelTiles.door6  },
                { TileEnum.DOOR8 , _levelTiles.door8  }
            };
        }
    }
}
