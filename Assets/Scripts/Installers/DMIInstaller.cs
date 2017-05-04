using Assets.Scripts.Level.TilesTranslator;
using Assets.Scripts.SceneCreator;
using Zenject;

public class DMIInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        //if we need any Unity instances passed to services bind them using this statement
        //Container.BindInstance(foo);

        //manager binding
        Container.Bind<DMIManager>().AsSingle();
        Container.Bind<IInitializable>().To<DMIManager>().AsSingle();
        Container.Bind<ITickable>().To<DMIManager>().AsSingle();
        Container.Bind<IFixedTickable>().To<DMIManager>().AsSingle();

        //services binding 
        Container.Bind<LevelGeneratorService>().AsSingle();
        Container.Bind<SceneCreatorService>().AsSingle();
        

        //generator service components binding
        Container.Bind<IDMILevelGenerator>().To<DMIDummyLevelGenerator>().AsSingle();
        Container.Bind<IDMIRoomGenerator>().To<DMIRegularRoomGenerator>().AsSingle();
        Container.Bind<IDMITilesTranslator>().To<DMIFancyBoundariesTilesTranslator>().AsSingle();

        //level spawner components
        Container.Bind<LevelTilesSpawner>().AsSingle();
        Container.Bind<LevelItemsSpawner>().AsSingle();
    }
}