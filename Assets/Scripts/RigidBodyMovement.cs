using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Collider ground;

    public float acceleration = 200f;
    public float maxSpeed = 7.5f;
    public bool useSprint = true;
    public bool reverse = false;

    public float jumpForce = 30f;
    private bool jumped = false;

    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        velocity = new Vector2();

        if (reverse)
            acceleration *= -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * (acceleration * 100) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.forward * (-acceleration * 100) * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(transform.right * (-acceleration * 100) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * (acceleration * 100) * Time.deltaTime);
        }

        velocity.x = rb.velocity.x;
        velocity.y = rb.velocity.z;

        if (useSprint && Input.GetKey(KeyCode.LeftShift))
        {
            if(velocity.magnitude > maxSpeed * 2)
                velocity = velocity.normalized * (maxSpeed * 2);
        }
        else if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !jumped)
        {
            rb.AddForce(Vector3.up * (jumpForce * 1000) * Time.deltaTime);
            jumped = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
            jumped = false;
    }
}
