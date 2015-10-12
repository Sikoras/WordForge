using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        if (Application.loadedLevel == 0)
        {
            NextScene();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextScene()
    {
        Application.LoadLevel(1);
    }
}
