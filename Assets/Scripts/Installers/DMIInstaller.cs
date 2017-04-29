using UnityEngine;
using Zenject;

public class DMIInstaller : MonoInstaller<DMIInstaller>
{
    public override void InstallBindings()
    {
        //manager binding
        Container.BindInterfacesAndSelfTo<DMIManager>().AsSingle();

        //generators bindings
        Container.BindInterfacesAndSelfTo<DMIDummyLevelGenerator>().AsSingle();
        Container.BindInterfacesAndSelfTo<DMIRegularRoomGenerator>().AsSingle();
    }
}