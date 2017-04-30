using UnityEngine;
using Zenject;

public class DMIInstaller : MonoInstaller<DMIInstaller>
{
    public override void InstallBindings()
    {
        //manager binding
        Container.BindInterfacesAndSelfTo<DMIManager>().AsSingle();

        //services binding 
        Container.Bind<LevelGeneratorService>().AsSingle();

        //generator service components binding
        Container.Bind<IDMILevelGenerator>().To<DMIDummyLevelGenerator>().AsSingle();
        Container.Bind<IDMIRoomGenerator>().To<DMIRegularRoomGenerator>().AsSingle();
    }
}