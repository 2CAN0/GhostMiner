using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Camera miniCam;
    public RenderTexture miniMap;

    public GameObject player;

    public float offset = 462.1f;
    public float fov = 5f;
    public bool useRotation = false;

    private Vector3 rotation;

    private void Start()
    {
        miniCam.forceIntoRenderTexture = true;
        miniCam.targetTexture = miniMap;
        miniCam.fieldOfView = fov;

        rotation = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        miniCam.transform.position = new Vector3(player.transform.position.x, offset, player.transform.position.z);

        if (useRotation)
        {
            rotation = new Vector3(90f, player.transform.eulerAngles.y, 0);
            miniCam.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
