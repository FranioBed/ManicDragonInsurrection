using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Inject]
    LevelManager _levelManager;
    [Inject]
    SettingsInstaller.GameSettings _settings;

    public Player playerPrefab;

    private int _seed;

    public void Start()
    {
        _seed = setSeed();
        _levelManager.injectWorkaround(_seed, playerPrefab);
        _levelManager.setLevel(0);
        _levelManager.goToNextLevel();
        playerPrefab.OnDeath += RollGameOver;
    }

    private void RollGameOver(object sender)
    {
        SceneManager.LoadScene("GameOver");
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
}
