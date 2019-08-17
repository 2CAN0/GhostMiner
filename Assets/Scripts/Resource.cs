using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Target
{
    public GameObject dropItem;

    private void Start()
    {
        if(dropItem == null)
        {
            dropItem = gameObject;
            dropItem.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }

    protected override void Die()
    {
        Instantiate(dropItem, transform.position, dropItem.transform.rotation);
        base.Die();
    }
}
