using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.SceneCreator;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    int seed;  //TODO: use as readonly property??


    public void Start()
    {
        seed = setSeed();
        LevelInfo levelInfo = _levelGeneratorSerivce.generate(seed);
        IList<Marker> markers = _sceneCreatorService.Create(levelInfo);
        setPlayerToStartPos(markers);
    }

    private void setPlayerToStartPos(IList<Marker> markers) //TODO: extract to another class (single-responsibility rule)
    {
        Marker startPos = markers.Where<Marker>(m => ItemOnTileEnum.STARTPOS.Equals(m.itemType)).First();
        float posX = startPos.position.x * _prefabSettings.tileSpan;
        float posY = -startPos.position.y * _prefabSettings.tileSpan;
        playerPrefab.transform.Translate(new Vector3(posX, posY, 0.0f));
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
