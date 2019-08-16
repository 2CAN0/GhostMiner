using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string uName;
    public float health;

    public bool devMode = false;

    private bool godMode = false;
    public Rigidbody rb;
    public Collider[] colliders;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (devMode)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                godMode = !godMode;
                rb.useGravity = !godMode;

                foreach (Collider col in colliders)
                    col.enabled = !godMode;

                Debug.Log("Using GodMode: " + godMode);
            }
        }
            
    }
}
