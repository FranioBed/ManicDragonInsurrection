using UnityEngine;
using Zenject;

public class ExitObject : MonoBehaviour {

    [Inject]
    GameManager _gameManager;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            _gameManager.goToNextLevel();
        }
    }
	
}
