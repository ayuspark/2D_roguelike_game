using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;
    public int PlayerFoodPoints = 100;
    [HideInInspector]
    public bool PlayersTurn = true;

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
	
    public void GameOver()
    {
        enabled = false;
    }
}
