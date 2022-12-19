using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public WeaponHandler weaponHandler;
    public ItemHandler itemHandler;
    public List<Item> invItems = new List<Item>();
    public static InventoryHandler Instance;

    //Gonna move this to simple control
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        AddItem("Sword");
        AddItem("Sword");
        AddItem("Potion");
        AddItem("Potion");
        AddItem("Potion");
        AddItem("Potion");
        AddItem("Potion");
        AddItem("Potion");
        AddItem("Potion");
        AddItem("Potion");
        AddItem("Potion");
    }

    public void EquipItem(Item equippableItem )
    {
        weaponHandler.Equip(equippableItem);
    }

    public void ConsumeItem(Item consumableItem)
    {
        itemHandler.ConsumeItem(consumableItem);
    }
    public void AddItem(string itemName)
    {
        Item item = ItemDB.Instance.GetItem(itemName);
        invItems.Add(item);
        UIEventManager.ItemAdded(item);
    }
    public void AddItem(Item item)
    {
        invItems.Add(item);
        UIEventManager.ItemAdded(item);
    }
    
}
