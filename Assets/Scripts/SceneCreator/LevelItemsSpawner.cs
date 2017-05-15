using System;
using System.Collections.Generic;
using Assets.Scripts.Level.LevelDTO;
using UnityEngine;

namespace Assets.Scripts.SceneCreator
{

    class LevelItemsSpawner : LevelAbstractSpawner<ItemOnTileEnum, SettingsInstaller.LevelItems>
    {
        protected override Dictionary<ItemOnTileEnum, GameObject> populateMapping()
        {
            return new Dictionary<ItemOnTileEnum, GameObject>()
            {
                { ItemOnTileEnum.CHEST, _configOfType.chest },
                { ItemOnTileEnum.EXIT , _configOfType.exit  }
            };
        }
    };
}
