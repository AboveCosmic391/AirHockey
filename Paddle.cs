using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public int playerNumber;

    public float paddleSpeed = 10f;

    private Rigidbody paddleRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        paddleRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal and vertical paddle movements 
        float horizontalMovement = Input.GetAxis("Horizontal" + playerNumber);
        float verticalMovement = Input.GetAxis("Vertical" + playerNumber);
        paddleRigidbody.velocity = new Vector3(horizontalMovement, 0, verticalMovement) * paddleSpeed;
    }
}
