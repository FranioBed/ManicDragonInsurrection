using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DMISettingsInstaller", menuName = "Installers/DMISettingsInstaller")]
public class DMISettingsInstaller : ScriptableObjectInstaller<DMISettingsInstaller>
{
    public GameSettings game;
    public LevelSettings level;
    public RoomSettings room;
    public CharacterSettings character;

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

    public override void InstallBindings()
    {
        Container.BindInstance(game);
        Container.BindInstance(level);
        Container.BindInstance(room);
        Container.BindInstance(character);
    }
}