using Assets.Scripts.SceneCreator;
using UnityEngine;
using Zenject;

public class ExitObject : MonoBehaviour {

    [Inject]
    LevelManager _levelManager;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            _levelManager.goToNextLevel();
        }
    }
	
}
