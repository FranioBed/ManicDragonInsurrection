using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.SceneCreator;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour {
    [Inject]
    LevelManager _levelManager;
    //TODO
    //AI
    //UIController
    //anything that somes to mind...
    [Inject]
    SettingsInstaller.GameSettings _settings;

    public GameObject playerPrefab;

    private int _seed;

    public void Start()
    {
        _seed = setSeed();
        _levelManager.injectWorkaround(_seed, playerPrefab);
        _levelManager.setLevel(0);
        _levelManager.goToNextLevel();
    }

    private int setSeed()
    {
        if (_settings.useFixedSeed)
            return _settings.fixedSeed;
        return new System.Random().Next();
    }

    public int getSeed()
    {
        return _seed;
    }



    public void FixedUpdate()
    {
        //todo
    }

    public void Update()
    {
        //todo
    }
}
