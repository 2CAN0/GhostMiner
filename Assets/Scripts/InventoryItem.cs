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

    private int itemCount;

    // Start is called before the first frame update
    void Start()
    {
        //inventoryItem = new GameObject();
        itemCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = inventoryItem.transform.rotation.eulerAngles;
        rotation.y += rotationSpeed;
        inventoryItem.transform.rotation = Quaternion.Euler(rotation);
    }

    public void ChangeSelected(GameObject selected, int itemCount)
    {
        this.itemCount = itemCount;
        Debug.Log("Changed to: " + selected.name.Split('_')[0] + ". There are " + this.itemCount + " in your inv");
      
        inventoryItem = selected;

        textCount.SetActive(true);
        
        textCount.GetComponent<TextMeshProUGUI>().text = itemCount+"";
        itemName.GetComponent<TextMeshProUGUI>().text = selected.name.Split('_')[0];
        StartCoroutine(TextFlip());
    }

    IEnumerator TextFlip()
    {
        itemName.SetActive(true);
        yield return new WaitForSeconds(2);
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
