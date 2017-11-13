using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MovingObject {

    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;

    public float restartLevelDelay = 1f;

    private Animator animator;
    private int food;

	// Use this for initialization
	protected override void Start () {
        animator.GetComponent<Animator>();
        food = GameManager.instance.PlayerFoodPoints;
        base.Start();
	}

    private void OnDisable()
    {
        GameManager.instance.PlayerFoodPoints = food;
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.PlayersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        // to prevent from moving diagonally 
        if(horizontal != 0)
        {
            vertical = 0;
        }
        if(vertical != 0 || horizontal != 0)
        {
            AttemptMove<WallScript>(horizontal, vertical);
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;
        base.AttemptMove<T>(xDir,yDir);
        RaycastHit2D hit;
        CheckIfGameOver();
        GameManager.instance.PlayersTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        WallScript hitWall = component as WallScript;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("Player_chop");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        } else if (other.tag == "Food")
        {
            food += pointsPerFood;
            other.gameObject.SetActive(false);
        } else if (other.tag == "Soda")
        {
            food += pointsPerSoda;
        }
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("Player_hit");
        food -= loss;
        CheckIfGameOver();
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}
