using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;

    private int level = 3;


	// Use this for initialization
	void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // so that scores and persist
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame(); 
	}

    void InitGame()
    {
        boardScript.SetupScenes(level);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
