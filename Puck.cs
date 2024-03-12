using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Puck : MonoBehaviour
{

    // goal stuff
    public delegate void GoalHandler(string scorer);
    public event GoalHandler OnGoal;

    // timer stuff
    public delegate void GameOverHandler();
    public event GameOverHandler OnGameOver;
    public float timeLeft = 30.0f;
    public bool timeOn = true;

    public Text timerText; 

    // puck stuff
    public float deceleration = 0.99f;
    public float startingHorizontalPosition;

    private Rigidbody puckRigidbody;
    private bool isCornered = false;
    private float timer = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        puckRigidbody = gameObject.GetComponent<Rigidbody>();   
        ResetPuckPosition(false);
        timeOn = true;
    }

    void Update()
    {
        if(isCornered)
        {
            Debug.Log("Puck is cornered! Time left: " + timer );
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                isCornered = false;
                timer = 3.0f;
                ResetPuckPosition(true); 
            }
            
        }

        // countdown timer
        if(timeOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                Debug.Log("Time left: " + timeLeft);
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timeOn = false;
                Debug.Log("Time is up!");
                if(OnGameOver != null)
                {
                    OnGameOver();
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        puckRigidbody.velocity = new Vector3(
            puckRigidbody.velocity.x * deceleration,
            puckRigidbody.velocity.y,
            puckRigidbody.velocity.z * deceleration 
        );
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /**
    Both objects must have Box Collider components attached to them.
    */
    void OnCollisionEnter(Collision collision)
    {
        puckRigidbody.velocity = Vector3.zero;
        Debug.Log("Collision name: " +  collision.gameObject.name); // name of object that collided
        Debug.Log("Goal tag: "+  collision.gameObject.tag); // tag of object that collided
        Debug.Log("Velocity: " + puckRigidbody.velocity); // velocity of the puck

        // play puck sound
        gameObject.GetComponent<AudioSource>().Play();

        if(collision.gameObject.CompareTag("Goal"))
        {
            OnGoal?.Invoke("Player");
        }

        if(collision.gameObject.tag == "CompGoal")
        {
            if(OnGoal != null)
            {
                OnGoal("Computer");
            }
        }
        if(collision.gameObject.tag == "Corner")
        {
            Debug.Log("Corner collision; Name: " + collision.gameObject.name);
            isCornered = true;
            ResetPuckPosition(true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision exited: " + collision.gameObject.name);
    }

    public void ResetPuckPosition(bool isLeft)
    {
        transform.position = new Vector3(
            -startingHorizontalPosition * (isLeft ? -1 : 1),
            transform.position.y,
            transform.position.z
        );
        puckRigidbody.velocity = Vector3.zero;
    }
}
