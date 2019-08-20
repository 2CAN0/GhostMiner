using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItem : MonoBehaviour
{
    public float bounceSpeed = 0.1f;
    public float center = 0.5f;
    public float height = 1f;

    private float angle = 0.0f;
    private bool startBounce = false;
    private Vector3 startPos;
    private Collider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        angle += bounceSpeed;

        if (startBounce)
        {
            float newY = startPos.y + height * Mathf.Sin(angle) / 2 + center;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            //Debug.Log("Disabled Gravity");
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            startBounce = true;
            startPos = transform.position;
        }



        if (collision.collider.tag == "Player")
        {
            //Debug.Log("Hit Player");
            Player player = collision.collider.GetComponent<Player>();
            if (player != null)
            {
                player.AddItem(gameObject);
            }

            Destroy(gameObject);
        }
    }
}
