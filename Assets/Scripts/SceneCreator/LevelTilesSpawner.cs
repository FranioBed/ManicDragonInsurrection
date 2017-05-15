using Assets.Scripts.Level.LevelDTO;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.SceneCreator
{
    
    public class LevelTilesSpawner : LevelAbstractSpawner<TileEnum,SettingsInstaller.LevelTiles>
    {

        override protected Dictionary<TileEnum, GameObject> populateMapping()
        {
             return new Dictionary<TileEnum, GameObject>() {
                { TileEnum.FLOOR1, _configOfType.floor1 },
                { TileEnum.FLOOR2, _configOfType.floor2 },
                { TileEnum.FLOOR3, _configOfType.floor3 },
                { TileEnum.WALL1 , _configOfType.wall1  },
                { TileEnum.WALL1X, _configOfType.wall1x },
                { TileEnum.WALL2 , _configOfType.wall2  },
                { TileEnum.WALL3 , _configOfType.wall3  },
                { TileEnum.WALL3X, _configOfType.wall3x },
                { TileEnum.WALL4 , _configOfType.wall4  },
                { TileEnum.WALL5 , _configOfType.wall5  },
                { TileEnum.WALL6 , _configOfType.wall6  },
                { TileEnum.WALL7 , _configOfType.wall7  },
                { TileEnum.WALL7X, _configOfType.wall7x },
                { TileEnum.WALL8 , _configOfType.wall8  },
                { TileEnum.WALL9 , _configOfType.wall9  },
                { TileEnum.WALL9X, _configOfType.wall9x },
                { TileEnum.DOOR2 , _configOfType.door2  },
                { TileEnum.DOOR4 , _configOfType.door4  },
                { TileEnum.DOOR6 , _configOfType.door6  },
                { TileEnum.DOOR8 , _configOfType.door8  }
            };
        }
    }
}
