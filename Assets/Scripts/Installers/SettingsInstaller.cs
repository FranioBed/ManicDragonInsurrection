using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    public GameSettings game;
    public LevelSettings level;
    public CharacterSettings character;
    public UsedPrefabs prefabs;

    [Serializable]
    public class GameSettings
    {
        public bool useFixedSeed;
        public int fixedSeed;
    }

    [Serializable]
    public class LevelSettings
    {
        public int minLevelSize;
        public int maxLevelSize;
        public float levelSizeVariance;
        public RoomSettings roomsSettings;
    }

    [Serializable]
    public class RoomSettings
    {
        public bool useFancyLayouts;
    }

    [Serializable]
    public class CharacterSettings
    {
        public int dummystat;
    }

    [Serializable]
    public class UsedPrefabs
    {
        public PrefabsConfig prefabConfig;
        public LevelTiles levelTiles;
        public LevelItems levelItems;
        public bool plzAddCharsAndItemsEtc;
    }

    [Serializable]
    public class PrefabsConfig
    {
        public float tileSpan;
    }

    [Serializable]
    public class LevelTiles
    {
        public GameObject floor1;
        public GameObject floor2;
        public GameObject floor3;
        public GameObject door2;
        public GameObject door4;
        public GameObject door6;
        public GameObject door8;
        public GameObject wall1;
        public GameObject wall1x;
        public GameObject wall2;
        public GameObject wall3;
        public GameObject wall3x;
        public GameObject wall4;
        public GameObject wall5;
        public GameObject wall6;
        public GameObject wall7;
        public GameObject wall7x;
        public GameObject wall8;
        public GameObject wall9;
        public GameObject wall9x;
    }

    [Serializable]
    public class LevelItems
    {
        public GameObject exit;
        public GameObject chest;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(game);
        Container.BindInstance(level);
        Container.BindInstance(level.roomsSettings);
        Container.BindInstance(character);
        Container.BindInstance(prefabs);
        Container.BindInstance(prefabs.prefabConfig);
        Container.BindInstance(prefabs.levelTiles);
        Container.BindInstance(prefabs.levelItems);
    }
}