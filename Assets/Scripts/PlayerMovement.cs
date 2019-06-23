using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    // Start is called before the first frame update
    public float acceleration;
    public float maxSpeed;
    public float jumpForce;
    public float slowDownSpeed = 5f;
    //public float slowDownRate = .5f;
    private float raycastDistance = 1.5f;
    private Vector3 gravityVector;
    public float xGrav;
    public float yGrav;
    public float zGrav;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        gravityVector = new Vector3(0, -yGrav, 0);
        // xGrav = playerRigidbody.velocity.x;
        //   zGrav = playerRigidbody.velocity.z;\
        //InvokeRepeating("SlowDown", 0, slowDownRate);
    }
    void FixedUpdate()
    {
       /* if (!IsGrounded())
        {
            Gravity();
        }*/

        Jump();
        SimpleMovement();
        //Movement();
        // Debug.Log("Velocity magnitude: " + playerRigidbody.velocity.magnitude);
        // SlowDown();

    }

    private void Gravity()
    {
        // playerRigidbody.AddRelativeForce(gravityVector * Time.deltaTime, ForceMode.VelocityChange);//, ForceMode.Acceleration);
        playerRigidbody.velocity += transform.TransformDirection(gravityVector) * Time.deltaTime;
        // playerRigidbody.velocity += (gravityVector) * Time.deltaTime;
        Debug.Log("Gravity should be happening!");

    }

    private void SimpleMovement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 forwardMovement = transform.forward * verticalMove;
        Vector3 sideMovement = transform.right * horizontalMove;

        playerRigidbody.velocity = new Vector3(horizontalMove * acceleration * Time.deltaTime, playerRigidbody.velocity.y, verticalMove * acceleration * Time.deltaTime);

    }

    private void Movement()
    {
        
   
        // playerRigidbody.velocity = new Vector3(horizontalMove * acceleration * Time.deltaTime, 0, verticalMove * acceleration * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))//Moving Forward
        {
            if (playerRigidbody.velocity.z < 0)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, 0);
            }
            if (playerRigidbody.velocity.z <= maxSpeed)
             { 
            //  playerRigidbody.velocity += transform.TransformDirection(new Vector3(0, 0, acceleration) * Time.deltaTime);
               // Debug.Log("Z VELOCITY from W: " + playerRigidbody.velocity.z);
             }
           // playerRigidbody.velocity += transform.TransformDirection(new Vector3(0, 0, verticalMove * acceleration) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))//Moving Backward
        {
            if(playerRigidbody.velocity.z > 0)
            {
               playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, 0);
            }
            if (playerRigidbody.velocity.z >= -maxSpeed)
            { 
                playerRigidbody.velocity -= (new Vector3(0, 0, acceleration) * Time.deltaTime);
            }
        }

        if(!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) //slowing forward and back
        {   
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, 0); 
        }





        if (Input.GetKey(KeyCode.A))//Moving Left
        {
            if (playerRigidbody.velocity.x > 0)
            {
                playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
            }
            if (playerRigidbody.velocity.x >= -maxSpeed)
            {
                playerRigidbody.velocity -= (new Vector3(acceleration, 0, 0) * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.D))//Moving Right
        {
            if (playerRigidbody.velocity.x < 0)
            {
                playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
            }
            if (playerRigidbody.velocity.x <= maxSpeed)
            {
                playerRigidbody.velocity += (new Vector3(acceleration, 0, 0) * Time.deltaTime);
  
            }
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) //slowing Left and Right
        {
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
        }
        /*if (playerRigidbody.velocity.x <= maxSpeed && playerRigidbody.velocity.x >= -maxSpeed)
        {
           // playerRigidbody.AddRelativeForce(horizontalMove * acceleration * Time.deltaTime, 0, 0, ForceMode.Impulse);

        }
        if (playerRigidbody.velocity.z <= maxSpeed && playerRigidbody.velocity.z >= -maxSpeed)
        {
         //   playerRigidbody.AddRelativeForce(0, 0, verticalMove * acceleration * Time.deltaTime, ForceMode.Impulse);

        }*/

    }

    private void SlowDown()
    {
        if (!(Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.S)))
        {
            //playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, playerRigidbody.velocity.z);

            //playerRigidbody.velocity = transform.TransformDirection(new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, 0));

            if (playerRigidbody.velocity.z > 0)
            {
                Debug.Log("Forward Speed too high, decreasing to a stop!");
                playerRigidbody.velocity -= new Vector3(0, 0, slowDownSpeed);
            }
            else if (playerRigidbody.velocity.z < 0)
            {
                Debug.Log("Backward Speed too high, decreasing to a stop!");
                playerRigidbody.velocity += new Vector3(0, 0, slowDownSpeed);
            }
            else if (playerRigidbody.velocity.z == 0)
            {
                Debug.Log("Vertical Speed was 0, YAY!");
            }

        }

        if (!(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D)))
        {
            // playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, playerRigidbody.velocity.z);

            // playerRigidbody.velocity = transform.TransformDirection(new Vector3(0, playerRigidbody.velocity.y, playerRigidbody.velocity.z));
            if (playerRigidbody.velocity.x > 0)
            {
                Debug.Log("Right Speed too high, decreasing to a stop!");
                playerRigidbody.velocity -= new Vector3(slowDownSpeed, 0, 0);
            }
            else if (playerRigidbody.velocity.x < 0)
            {
                Debug.Log("Left Speed too high, decreasing to a stop!");
                playerRigidbody.velocity += new Vector3(slowDownSpeed, 0, 0);
            }
            else if (playerRigidbody.velocity.x == 0)
            {
                Debug.Log("Horizontal Speed was 0, YAY!");
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                playerRigidbody.AddRelativeForce(0, jumpForce, 0);// ForceMode.Impulse);
            }

        }
    }

    private bool IsGrounded()
    {

        return Physics.Raycast(transform.position, Vector3.down, gameObject.GetComponent<Collider>().bounds.extents.y + .1f); //raycastDistance);
    }

}
