using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{
    public Item DroppedItem { get; set; }
    public override void Interact()
    {
        Debug.Log("Interact with Item");
        if(DroppedItem!= null)
            InventoryHandler.Instance.AddItem(DroppedItem);
        Destroy(gameObject);
    }
}
