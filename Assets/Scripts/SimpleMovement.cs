using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float MovementSpeed = 2.5f;
    public bool reverse = false;

    public bool useHop = false;
    public bool useSprint = false;


    // Start is called before the first frame update
    void Start()
    {
        if (reverse)
            MovementSpeed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") && useSprint && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * MovementSpeed * 2.5f;
        }
        else if (Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * MovementSpeed;
        }

        if(Input.GetKey("s") && useSprint && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * MovementSpeed * 2.5f;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * MovementSpeed;
        }

        if(Input.GetKey("a"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * MovementSpeed;
        }
        if (Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * MovementSpeed;
        }
    }
}
