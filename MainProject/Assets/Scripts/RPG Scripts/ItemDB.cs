using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ItemDB : MonoBehaviour
{
    public static ItemDB Instance { get; set; }
    private List<Item> Items { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        ImportFromDB();
    }

    void ImportFromDB()
    {
        Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("ItemDB").ToString());
    }

    public Item GetItem(string itemName)
    {
        foreach (var item in Items)
        {
            Debug.Log(item.ItemName);
            if (item.ItemName == itemName)
                return item;
        }
        return null;
    }
}
