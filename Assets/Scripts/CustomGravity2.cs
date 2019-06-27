using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity2 : MonoBehaviour
{
    // Gravity Scale editable on the inspector
    // providing a gravity scale per object

    public float gravityScale = 1.0f;
    public enum GravityDirection { Up, Down, Left, Right, Forward, Back };
    private Vector3 gravityActual;
    public GravityDirection gravDirection;
    private bool cameraFlipOnce = true;
    private GameObject cameraObj;
    public Vector3 correctUp;
    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -9.81f;

    Rigidbody playerRigidBody;

    void OnEnable()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.useGravity = false;
        gravDirection = GravityDirection.Down;
        gravityActual = new Vector3(0, globalGravity * gravityScale, 0);
        cameraObj = GameObject.Find("Camera");

    }

    void FixedUpdate()
    {
        GravityTest();

       
        if (gravDirection == GravityDirection.Down)
        {
            // Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            gravityActual = new Vector3(0, globalGravity * gravityScale, 0);
            playerRigidBody.AddForce(gravityActual, ForceMode.Acceleration);
            if (cameraFlipOnce)
            {
                //transform.rotation.SetEulerAngles(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                transform.localEulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);


                //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                // transform.Rotate(0, 0, 180);

              //  correctUp = transform.up;
                cameraFlipOnce = false;
            }

        }
        else if (gravDirection == GravityDirection.Up)
        {
            // Vector3 gravity = globalGravity * gravityScale * -Vector3.up;
            gravityActual = new Vector3(0, (-globalGravity * gravityScale), 0);
            playerRigidBody.AddForce(gravityActual, ForceMode.Acceleration);
            if (cameraFlipOnce)
            {
                // transform.rotation.SetEulerAngles(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180);
                transform.localEulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180);

                cameraFlipOnce = false;
               // correctUp = //transform.up;

            }

        }
        else if (gravDirection == GravityDirection.Forward)
        {
            // Vector3 gravity = globalGravity * gravityScale * -Vector3.forward;
             gravityActual = new Vector3(0, 0, (-globalGravity * gravityScale));
           
            playerRigidBody.AddForce(gravityActual, ForceMode.Acceleration);
            if (cameraFlipOnce)
            {

                transform.localEulerAngles = new Vector3(-90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                cameraFlipOnce = false;
            //    correctUp = transform.up;

            }
        }
        else if (gravDirection == GravityDirection.Back)
        {
            //  Vector3 gravity = globalGravity * gravityScale * Vector3.forward;
              gravityActual = new Vector3(0, 0, (globalGravity * gravityScale));
           

            playerRigidBody.AddForce(gravityActual, ForceMode.Acceleration);
            if (cameraFlipOnce)
            {

                transform.localEulerAngles = new Vector3(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                cameraFlipOnce = false;
                correctUp = transform.up;

            }
        }
        else if (gravDirection == GravityDirection.Right)
        {
           
            //  Vector3 gravity = globalGravity * gravityScale * -Vector3.right;
            gravityActual = new Vector3((-globalGravity * gravityScale), 0, 0);
            playerRigidBody.AddForce(gravityActual, ForceMode.Acceleration);
            if (cameraFlipOnce)
            {
                transform.localEulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 90);
                cameraFlipOnce = false;
               // correctUp = transform.up;

            }
        }
        else if (gravDirection == GravityDirection.Left)
        {
           
            //  Vector3 gravity = globalGravity * gravityScale * Vector3.right;
            gravityActual = new Vector3((globalGravity * gravityScale), 0, 0);
            playerRigidBody.AddForce(gravityActual, ForceMode.Acceleration);


            if (cameraFlipOnce)
            {
               
                transform.localEulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -90);
                cameraFlipOnce = false;
           //     correctUp = transform.up;

            }
        }

       // transform.up = correctUp;

    }


    private void GravityTest()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gravDirection = GravityDirection.Down;
            cameraFlipOnce = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gravDirection = GravityDirection.Up;
            cameraFlipOnce = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gravDirection = GravityDirection.Forward;
            cameraFlipOnce = true;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gravDirection = GravityDirection.Back;
            cameraFlipOnce = true;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            gravDirection = GravityDirection.Left;
            cameraFlipOnce = true;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            gravDirection = GravityDirection.Right;
            cameraFlipOnce = true;

        }
    }
}
