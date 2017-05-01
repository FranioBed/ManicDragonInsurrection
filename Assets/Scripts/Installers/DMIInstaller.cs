using Assets.Scripts.Level.TilesTranslator;
using UnityEngine;
using Zenject;

public class DMIInstaller : MonoInstaller<DMIInstaller>
{

    public override void InstallBindings()
    {
        //manager binding
        Container.Bind<DMIManager>().AsSingle();
        Container.Bind<IInitializable>().To<DMIManager>().AsSingle();
        Container.Bind<ITickable>().To<DMIManager>().AsSingle();
        Container.Bind<IFixedTickable>().To<DMIManager>().AsSingle();

        //services binding 
        Container.Bind<LevelGeneratorService>().AsSingle();

        //generator service components binding
        Container.Bind<IDMILevelGenerator>().To<DMIDummyLevelGenerator>().AsSingle();
        Container.Bind<IDMIRoomGenerator>().To<DMIRegularRoomGenerator>().AsSingle();
        Container.Bind<IDMITilesTranslator>().To<DMISimpleTilesTranslator>().AsSingle(); //TODO: Replace with FancyBoundaries impl
    }
}