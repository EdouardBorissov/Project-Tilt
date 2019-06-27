using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoinkedPlayerMove : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;
    private Rigidbody playerRigidbody;
    private CustomGravity2 playerGravity;
   



    void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        playerRigidbody.useGravity = false;
        playerGravity = gameObject.GetComponent<CustomGravity2>();
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            Vector3 targetVelocity = new Vector3(0,0,0);
            // Calculate how fast we should be moving

            targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = playerRigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
          //  velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            //velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            //velocityChange.y = 0;
             playerRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
     

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                // playerRigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                playerRigidbody.AddRelativeForce(0, CalculateJumpVerticalSpeed(), 0, ForceMode.Impulse);
            }
        }

        // We apply gravity manually for more tuning control
        //playerRigidbody.AddForce(new Vector3(0, -gravity * playerRigidbody.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
