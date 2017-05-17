using Assets.Scripts.Level.TilesTranslator;
using Assets.Scripts.SceneCreator;
using Zenject;

public class Installer : MonoInstaller
{

    public override void InstallBindings()
    {
        //if we need any Unity instances passed to services bind them using this statement
        //Container.BindInstance(foo);

        //manager binding
        Container.Bind<GameManager>().AsSingle();
        //Container.Bind<IInitializable>().To<GameManager>().AsSingle();
        //Container.Bind<ITickable>().To<GameManager>().AsSingle();
        //Container.Bind<IFixedTickable>().To<GameManager>().AsSingle();

        //services binding 
        Container.Bind<LevelGeneratorService>().AsSingle();
        Container.Bind<SceneCreatorService>().AsSingle();


        //generator service components binding
        //Container.Bind<ILevelGenerator>().To<DummyLevelGenerator>().AsSingle();
        Container.Bind<ILevelGenerator>().To<MapModelGenerator>().AsSingle();
        Container.Bind<IIRoomGenerator>().To<RegularRoomGenerator>().AsSingle();
        Container.Bind<ITilesTranslator>().To<FancyBoundariesTilesTranslator>().AsSingle();

        //level spawner components
        Container.Bind<LevelTilesSpawner>().AsSingle();
        Container.Bind<LevelItemsSpawner>().AsSingle();
        Container.Bind<LevelItemMarker>().AsSingle();
    }
}