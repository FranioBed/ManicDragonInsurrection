using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void loadCharacterChoiceScene()
    {
        SceneManager.LoadScene("CharacterChoice");
    }

    public void loadGame()
    {
        SceneManager.LoadScene("PlayArea");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
