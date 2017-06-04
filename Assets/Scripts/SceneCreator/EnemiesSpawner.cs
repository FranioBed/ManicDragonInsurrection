using Assets.Scripts.Level.LevelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.SceneCreator
{
    class EnemiesSpawner : LevelAbstractSpawner<ItemOnTileEnum, SettingsInstaller.Enemies>
    {
        protected override Dictionary<ItemOnTileEnum, GameObject> populateMapping()
        {
            return new Dictionary<ItemOnTileEnum, GameObject>()
            {
                { ItemOnTileEnum.ENEMY_1, _configOfType.enemy1 },
                { ItemOnTileEnum.ENEMY_2, _configOfType.enemy2 },
                { ItemOnTileEnum.ENEMY_3, _configOfType.enemy3 }
            };
        }
    };
}
