using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public GameObject warning;
    public int itemsToUse = 1;

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

        if (Input.GetKeyDown(KeyCode.U))
        {
            UseItem(inventory[GetInvKey(selectedIndex)][0], itemsToUse);
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
                        UpdateItemCount(selectedIndex);
                }
                else
                {
                    List<GameObject> gobjs = new List<GameObject> { resource };
                    inventory.Add(gobjName, gobjs);

                    // <= 1 since the item is already added to your inventory if this statement was before the add to list it would be <= 0
                    if (inventory.Count <= 1)
                    {
                        ChangeSelected(gobjName);
                    }
                }
            }
        }

        PrintItemList(inventory);
    }

    public void UpdateItemCount(int index)
    {
        invItemSpawn.GetComponent<InventoryItem>().itemCount = inventory[GetInvKey(index)].Count;
        invItemSpawn.GetComponent<InventoryItem>().textCount.GetComponent<TextMeshProUGUI>().text = invItemSpawn.GetComponent<InventoryItem>().itemCount.ToString();
    }

    public void ChangeSelected(string key)
    {
        Debug.Log("Hello World");
        InventoryItem invItem = invItemSpawn.GetComponent<InventoryItem>();

        invItem.ChangeSelected(inventory[key][0], inventory[key].Count);
    }

    public void UseItem(GameObject gobj, int amount)
    {

        string gobjName = gobj.name;
        if (gobj.name.Contains("_"))
        {
            gobjName = gobjName.Split('_')[0];
        }

        if (inventory.ContainsKey(gobjName))
        {
            //Checking if the user has enhough items of a certain resource
            if ((inventory[gobjName].Count) - amount >= 0)
            {
                for (int iGobj = amount; iGobj > 0; iGobj--)
                {
                    inventory[gobjName].RemoveAt(inventory[gobjName].Count - 1);
                    Debug.Log("Used item: " + gobjName);
                }

                if(inventory[gobjName].Count <= 0)
                {
                    inventory.Remove(gobjName);
                }

                invItemSpawn.GetComponent<InventoryItem>().UseItem(amount);

                PrintItemList(inventory);
            }
            else
            {
                TextMeshProUGUI textWarning = warning.GetComponent<TextMeshProUGUI>();
                if (textWarning != null)
                {
                    textWarning.text = "You need more resources to do this!";
                    StartCoroutine(ShowWarning());
                }
            }
        }
        else
        {
            Debug.Log("No Item in list");
        }
    }

    IEnumerator ShowWarning()
    {
        warning.SetActive(true);
        //Debug.Log("Showing warning");
        yield return new WaitForSeconds(2);
        warning.SetActive(false);
    }
}
