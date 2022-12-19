using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    private WeaponHandler weaponHandler;
    private ConsumableHandler consumableHandler;
    private CharacterStats characterStats;
    public List<Item> invItems = new List<Item>();
    public static InventoryHandler Instance;

    Transform handTransform = null;

    private void Awake()
    {
        handTransform = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand);
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        characterStats = gameObject.AddComponent<CharacterStats>();
        weaponHandler = gameObject.AddComponent<WeaponHandler>();
        weaponHandler.WeaponPos = handTransform;
        consumableHandler = gameObject.AddComponent<ConsumableHandler>();
    }

    private void Start()
    {
        //AddItem("Sword");
    }

    public void EquipItem(Item equippableItem)
    {
        weaponHandler.Equip(equippableItem);
    }

    public void ConsumeItem(Item consumableItem)
    {
        consumableHandler.ConsumeItem(consumableItem);
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
    public void AddItemsToInventory(List<Item> i_invItems)
    {
        foreach (Item item in i_invItems)
        {
            AddItem(item);
        }
    }

}
