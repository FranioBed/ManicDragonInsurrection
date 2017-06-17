using System;
using Assets.Scripts.Level.LevelDTO;
using Assets.Scripts.SceneCreator;
using UnityEngine;
using Zenject;

public class LevelManager
{
    [Inject]
    LevelGeneratorService _levelGeneratorSerivce;
    [Inject]
    SceneCreatorService _sceneCreatorService;
    [Inject]
    SettingsInstaller.PrefabsConfig _prefabSettings;

    private int _level = 0;
    private LevelGameObjectsHolder _holder = new LevelGameObjectsHolder();

    private GameObject _playerPrefab;
    private int _seed;

    public delegate void ValueChangedEventHandler(object sender, int value);
    public event ValueChangedEventHandler LevelChanged;

    public void injectWorkaround(int seed, GameObject playerPrefab)
    //FIXME: find smart way to inject values from GameManager into this object
    {
        _seed = seed;
        _playerPrefab = playerPrefab;
    }

    public void goToNextLevel()
    {
        _level++;
        Debug.Log("Loading level " + _level.ToString());
        LevelInfo levelInfo = _levelGeneratorSerivce.generate(_seed + _level);
        _holder = _sceneCreatorService.Create(_holder, levelInfo);
        setPlayerToStartPos(_holder.startPos);
        LevelChanged(this, _level);
    }

    public void setLevel(int v)
    {
        _level = v;
    }

    public int getLevel()
    {
        return _level;
    }

    private void setPlayerToStartPos(Marker startPos)
    {
        float posX = startPos.position.x * _prefabSettings.tileSpan;
        float posY = -startPos.position.y * _prefabSettings.tileSpan;
        _playerPrefab.transform.position = new Vector3(posX, posY, 0.0f);
    }
}
