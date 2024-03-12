using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Puck puck;
    public Text humanScoreText;
    public Text computerScoreText;
    public Text winText;

    private int humanScore = 0;
    private int computerScore = 0;

    private bool gameOver = false;
    public float endTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        puck.OnGoal += OnGoal;
        puck.OnGameOver += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            endTime -= Time.deltaTime;
            if(endTime <= 0)
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void OnGameOver()
    {
        Debug.Log("Game Over!");
        if(humanScore > computerScore)
        {
            Debug.Log("Human wins!");
            winText.text = "Human wins!";
        }

        else if(computerScore > humanScore)
        {
            Debug.Log("Computer wins!");
            winText.text = "Computer wins!";
        }

        else
        {
            Debug.Log("It's a tie!");
            winText.text = "It's a tie!";
        }
        gameOver = true;

    }

    void OnGoal(string scorer)
    {
        Debug.Log("A goal was scored!"); // log the goal

        // human scored
        if(scorer == "Player")
        {
            humanScore++;
            Debug.Log("Human: " + humanScore);
            Debug.Log("Computer: " + computerScore);
            // Destroy(puck.gameObject);
            puck.ResetPuckPosition(false);
            humanScoreText.text = humanScore.ToString();
        }
        // computer scored
        else
        {
            computerScore++;
            Debug.Log("Human: " + humanScore);
            Debug.Log("Computer: " + computerScore);
            // Destroy(puck.gameObject);
            puck.ResetPuckPosition(true);
            computerScoreText.text = computerScore.ToString();
        }
        
    }
}
