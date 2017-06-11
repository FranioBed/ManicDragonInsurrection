using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.SceneCreator;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using System.Linq;

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
    [Inject]
    SettingsInstaller.PrefabsConfig _prefabSettings;

    public GameObject playerPrefab;

    private int _seed;
    private int _level;
    private LevelGameObjectsHolder _holder;

    public void Start()
    {
        _seed = setSeed();
        _level = 0;
        _holder = new LevelGameObjectsHolder();
        goToNextLevel();
    }

    public void goToNextLevel()
    {
        _level++;
        LevelInfo levelInfo = _levelGeneratorSerivce.generate(_seed);
        _holder = _sceneCreatorService.Create(_holder, levelInfo);
        setPlayerToStartPos(_holder.startPos);
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

    public int getLevel()
    {
        return _level;
    }

    private void setPlayerToStartPos(Marker startPos) //TODO: extract to another class (single-responsibility rule)
    {
        float posX = startPos.position.x * _prefabSettings.tileSpan;
        float posY = -startPos.position.y * _prefabSettings.tileSpan;
        playerPrefab.transform.Translate(new Vector3(posX, posY, 0.0f));
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
