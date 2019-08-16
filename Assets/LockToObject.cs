using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToObject : MonoBehaviour
{
    public GameObject otherObject;

    public bool useRotation = true;

    private Vector3 rotationOffset;

    private void Start()
    {
        rotationOffset = this.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = otherObject.transform.position;

        if (useRotation)
        {
            this.transform.rotation = Quaternion.Euler(otherObject.transform.rotation.eulerAngles + rotationOffset);
        }
    }
}
