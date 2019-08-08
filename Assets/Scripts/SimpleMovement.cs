using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float MovementSpeed = 2.5f;
    public bool useSprint = false;
    private bool spaceDown = false;
    public float JumpForce = 12f;
    private float jumpyForce = 0;
    public float Gravity = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.Space) && !spaceDown)
        {
            spaceDown = true;
            jumpyForce = JumpForce;
        }

        if (jumpyForce != -JumpForce && spaceDown)
        {
            jumpyForce -= Gravity;
            transform.position += transform.TransformDirection(Vector3.up) * Time.deltaTime * jumpyForce;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                spaceDown = false;
                jumpyForce = 0;
            }
        }

        if (Input.GetKey("w") && useSprint && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * MovementSpeed * 2.5f;
        }
        else if (Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * MovementSpeed;
        }

        if (Input.GetKey("s") && useSprint && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * MovementSpeed * 2.5f;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * MovementSpeed;
        }

        if (Input.GetKey("a"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * MovementSpeed;
        }
        if (Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * MovementSpeed;
        }
    }
}
