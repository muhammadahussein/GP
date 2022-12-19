using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{
    public Item DroppedItem { get; set; }

    public string itemName;

    public override void Interact()
    {
        base.Interact();
        InventoryHandler.Instance.AddItem(itemName);
        Destroy(gameObject);
    }
}
