using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.SceneCreator;
using Zenject;

public class DMIManager : IInitializable, ITickable, IFixedTickable
{
    [Inject]
    LevelGeneratorService _levelGeneratorSerivce;
    [Inject]
    SceneCreatorService _sceneCreatorService;
    //TODO
    //AI
    //UIController
    //anything that somes to mind...

    [Inject]
    DMISettingsInstaller.GameSettings _settings;

    int seed;  //TODO: use as readonly property??


    public void Initialize()
    {
        seed = setSeed();
        LevelInfo levelInfo = _levelGeneratorSerivce.generate(seed);
        _sceneCreatorService.Create(levelInfo);
    }

    private int setSeed()
    {
       if (_settings.useFixedSeed)
            return _settings.fixedSeed;
       return new System.Random().Next();
    }

    public void FixedTick()
    {
        //todo
    }

    public void Tick()
    {
        //todo
    }
}
