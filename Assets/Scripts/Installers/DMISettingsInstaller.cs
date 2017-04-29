using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DMISettingsInstaller", menuName = "Installers/DMISettingsInstaller")]
public class DMISettingsInstaller : ScriptableObjectInstaller<DMISettingsInstaller>
{
    public LevelSettings level;
    public RoomSettings room;
    public CharacterSettings character;

    [Serializable]
    public class LevelSettings
    {
        public int dummystat;
    }

    [Serializable]
    public class RoomSettings
    {
        public bool useFancyLayouts;
        public float layoutVariance;
        public bool generateCrazyLoot;
    }

    [Serializable]
    public class CharacterSettings
    {
        public int dummystat;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(level);
        Container.BindInstance(room);
        Container.BindInstance(character);
    }
}