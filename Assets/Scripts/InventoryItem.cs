using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public GameObject textCount;
    public GameObject itemName;   
    public GameObject inventoryItem;

    public int itemCount;
    public string gobjName;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        //inventoryItem = new GameObject();
        itemCount = 0;
        tf = inventoryItem.transform;
        textCount.SetActive(false);
        inventoryItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = inventoryItem.transform.rotation.eulerAngles;
        rotation.y += rotationSpeed;
    }

    public void ChangeSelected(GameObject selected, int itemCount)
    {
        this.itemCount = itemCount;
        Debug.Log("Changed to: " + selected.name.Split('_')[0] + ". There are " + this.itemCount + " in your inv");

        inventoryItem.GetComponent<MeshFilter>().mesh = selected.GetComponent<MeshFilter>().mesh;
        inventoryItem.GetComponent<MeshRenderer>().materials = selected.GetComponent<MeshRenderer>().materials;

        inventoryItem.transform.localScale = selected.transform.localScale;
        inventoryItem.SetActive(true);

        textCount.SetActive(true);
        textCount.GetComponent<TextMeshProUGUI>().text = itemCount+"";

        gobjName = selected.name.Split('_')[0];
        itemName.GetComponent<TextMeshProUGUI>().text = gobjName;

        StartCoroutine(TextFlip());
    }

    IEnumerator TextFlip()
    {
        itemName.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        itemName.SetActive(false);
    }

    public void UsedItem()
    {
        itemCount -= 1;
        textCount.GetComponent<TextMeshProUGUI>().text = itemCount.ToString();
        if(itemCount <= 0)
        {
            textCount.SetActive(false);
            itemName.SetActive(false);
        }
    }
}
