using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject[] players;
    private float[] distance;

    private float smallesDistance = 0;
    private int index = 0;

    public float range = 20f;
    public float acceleration = 200f;
    public float maxSpeed = 7.5f;

    private Rigidbody rb;
    private Vector2 velocity;

    private void Start()
    {
        distance = new float[players.Length];
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int iGobj = 0; iGobj < players.Length; iGobj++)
        {
            Vector3 delta = players[iGobj].transform.position - this.transform.position;
            distance[iGobj] = delta.magnitude;

            if (iGobj == 0)
            {
                index = iGobj;
                smallesDistance = delta.magnitude;
            }

            if (delta.magnitude < smallesDistance)
            {
                index = iGobj;
                smallesDistance = delta.magnitude;
            }
        }

        this.transform.LookAt(players[index].transform);


        if(smallesDistance <= range)
        {
            rb.AddForce(transform.forward * (acceleration * 100) * Time.deltaTime);
        }

        velocity.x = rb.velocity.x;
        velocity.y = rb.velocity.z;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.y);
    }
}
