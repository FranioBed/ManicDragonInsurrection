using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.SceneCreator;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class PrefabInstaller : MonoInstaller
    {
        [Inject]
        SettingsInstaller.UsedPrefabs _prefabs;

        public override void InstallBindings()
        {
            //install prefabs factories here (character, enemies etc)
            /*
            Container.BindFactory<TileEnum, LevelTile, LevelTile.Factory>()
                .FromComponentInNewPrefab(_prefabs.levelTiles.floor1)
                .WithGameObjectName("Tile")
                .UnderTransformGroup("Tiles");
                */
        }
    }
}
