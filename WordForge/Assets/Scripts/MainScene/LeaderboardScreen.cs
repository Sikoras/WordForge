using UnityEngine;
using System.Collections;

public class LeaderboardScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoBack()
    {
        Application.LoadLevel(1);
    }
}
