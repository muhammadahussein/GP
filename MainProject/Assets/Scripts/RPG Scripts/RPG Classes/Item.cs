using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Item
{
    public List<Stat> Stats { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public enum ItemUsages { Weapon, Consumable, Quest }
    //converts enum indices to string
    [JsonConverter((typeof(Newtonsoft.Json.Converters.StringEnumConverter)))]
    public ItemUsages ItemUsage { get; set; }

    public Item(List<Stat> Stats, string ItemName)
    {
        this.Stats = Stats;
        this.ItemName = ItemName;
    }

    [Newtonsoft.Json.JsonConstructor]
    public Item(List<Stat> Stats, string ItemName, string ItemDescription, ItemUsages ItemUsage)
    {
        this.Stats = Stats;
        this.ItemName = ItemName;
        this.ItemDescription = ItemDescription;
        this.ItemUsage = ItemUsage;
    }
}



