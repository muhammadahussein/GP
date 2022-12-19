using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool isActive;
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private RectTransform inventoryScroller;
    private ItemSlot Slot { get; set; }

    public static InventoryUI Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;


        Slot = Resources.Load<ItemSlot>("UI/ItemSlot");
        UIEventManager.OnItemAdded += ItemAdded;
        inventoryScroller.gameObject.SetActive(false);
    }

    public void ItemAdded(Item item)
    {
        ItemSlot newSlot = Instantiate(Slot);
        newSlot.SetItem(item);
        newSlot.transform.SetParent(inventoryPanel);
        newSlot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            isActive = !isActive;
            MouseCam.EnableCursor(isActive);
            if (!isActive) Tooltip.HideTooltip();
            inventoryScroller.gameObject.SetActive(isActive);
        }
    }
    public void EnableInv()
    {
            isActive = !isActive;
            MouseCam.EnableCursor(isActive);
            if (!isActive) Tooltip.HideTooltip();
            inventoryScroller.gameObject.SetActive(isActive);
       

    }

    public void RemoveQuestSlot()
    {
        foreach (ItemSlot IS in inventoryPanel.GetComponentsInChildren<ItemSlot>())
        {
            if (IS.item.ItemUsage == Item.ItemUsages.Quest)
            {
                Destroy(IS.gameObject);
                InventoryHandler.Instance.invItems.Remove(IS.item);
            }
        }
    }
}
