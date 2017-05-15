using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.SceneCreator;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour { 
    [Inject]
    LevelGeneratorService _levelGeneratorSerivce;
    [Inject]
    SceneCreatorService _sceneCreatorService;
    //TODO
    //AI
    //UIController
    //anything that somes to mind...
    [Inject]
    SettingsInstaller.GameSettings _settings;

    public GameObject playerPrefab;

    int seed;  //TODO: use as readonly property??


    public void Start()
    {
        seed = setSeed();
        LevelInfo levelInfo = _levelGeneratorSerivce.generate(seed);
        IEnumerable<Marker> markers = _sceneCreatorService.Create(levelInfo);
    }

    private int setSeed()
    {
       if (_settings.useFixedSeed)
            return _settings.fixedSeed;
       return new System.Random().Next();
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
