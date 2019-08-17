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

    public Dictionary<string, List<GameObject>> inventory;
    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Dictionary<string, List<GameObject>>();
        spawnPosition = transform.position;
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

            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.position = spawnPosition;
            }
        }
            
    }

    public void PrintItemList(Dictionary<string, List<GameObject>> dict)
    {
        foreach(string key in dict.Keys)
        {
            print(key + ": " + dict[key].Count);
        }
    }

    public void AddItem(GameObject gobj)
    {
        string gobjName = gobj.name;
        if (gobj.name.Contains("_"))
        {
            gobjName = gobjName.Split('_')[0];
        }

        if (inventory.ContainsKey(gobjName))
        {
            inventory[gobjName].Add(gobj);
        }
        else
        {
            List<GameObject> gobjs = new List<GameObject>();
            gobjs.Add(gobj);
            inventory.Add(gobjName, gobjs);
        }

        PrintItemList(inventory);
    }

    public void UseItem(GameObject gobj)
    {
        string gobjName = gobj.name;
        if (gobj.name.Contains("_"))
        {
            gobjName = gobjName.Split('_')[0];
        }

        if (inventory.ContainsKey(gobjName))
        {
            inventory[gobjName].RemoveAt(inventory[gobjName].Count - 1);
        }
        else
        {
            Debug.Log("No Item in list");
        }

        PrintItemList(inventory);
    }
}
