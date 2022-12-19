using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool isActive;
    [SerializeField] private RectTransform inventoryPanel;
    private ItemSlot Slot { get; set; }
    
    private void Start()
    {
        Slot = Resources.Load<ItemSlot>("UI/ItemSlot");
        UIEventManager.OnItemAdded += ItemAdded;
        inventoryPanel.gameObject.SetActive(false);
    }

    public void ItemAdded(Item item)
    {
        ItemSlot newSlot = Instantiate(Slot);
        newSlot.SetItem(item);
        newSlot.transform.SetParent(inventoryPanel);
        newSlot.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isActive = !isActive;
            inventoryPanel.gameObject.SetActive(isActive);
        }
    }
}
