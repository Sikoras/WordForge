﻿using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAgain()
    {
        Application.LoadLevel(2);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
