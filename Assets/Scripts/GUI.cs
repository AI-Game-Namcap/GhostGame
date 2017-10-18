using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void StartScreen()
    {
        SceneManager.LoadScene("Start Screen", LoadSceneMode.Single);
    }
}
