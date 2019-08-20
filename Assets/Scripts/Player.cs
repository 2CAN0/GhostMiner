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
    public GameObject[] resources;

    public GameObject invItemSpawn;
    private int selectedIndex = 0;
    private int prevSelectedIndex = 0;

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

        if (inventory.Count > 0)
        {
            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            selectedIndex += (int)mouseWheel;
            //Debug.Log("MouseWheel: "+mouseWheel);

            if (selectedIndex >= inventory.Count)
            {
                selectedIndex = inventory.Count - 1;
            }
            else if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }

            if (selectedIndex != prevSelectedIndex)
                ChangeSelected(GetInvKey(selectedIndex));

            prevSelectedIndex = selectedIndex;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            UseItem(inventory["Stone"][0]);
        }
    }

    private string GetInvKey(int index)
    {
        if (index < inventory.Count)
        {
            string key = null;
            int i = 0;
            foreach (string k in inventory.Keys)
            {
                if (i == index)
                {
                    key = k;
                    break;
                }
                i += 1;
            }
            return key;
        }
        else
        {
            return null;
        }
    }

    public void PrintItemList(Dictionary<string, List<GameObject>> dict)
    {
        foreach (string key in dict.Keys)
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

        foreach (GameObject resource in resources)
        {
            if (resource.name.Contains(gobjName))
            {
                if (inventory.ContainsKey(gobjName))
                {
                    inventory[gobjName].Add(resource);

                    if (gobjName == invItemSpawn.GetComponent<InventoryItem>().gobjName)
                        invItemSpawn.GetComponent<InventoryItem>().itemCount++;
                }
                else
                {
                    List<GameObject> gobjs = new List<GameObject>();
                    gobjs.Add(resource);
                    inventory.Add(gobjName, gobjs);

                    //if(inventory.Count <= 0)
                    ChangeSelected(gobjName);
                }
            }
        }

        PrintItemList(inventory);
    }

    public void ChangeSelected(string key)
    {
        Debug.Log("Hello World");
        InventoryItem invItem = invItemSpawn.GetComponent<InventoryItem>();

        invItem.ChangeSelected(inventory[key][0], inventory[key].Count);
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
