using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void QuitApp()
    {
        Application.Quit();
    }

    public void PlayApp()
    {
        Application.LoadLevel(4);
    }

    public void SettingsApp()
    {
        Application.LoadLevel(2);
    }

    public void LeaderboardApp()
    {
        Application.LoadLevel(3);
    }
}
