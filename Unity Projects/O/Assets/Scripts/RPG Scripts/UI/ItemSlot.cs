using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        transform.Find("ItemIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Icons/" + item.ItemName);
    }

    public void UseItem()
    {
        switch (item.ItemUsage)
        {
            case Item.ItemUsages.Consumable:
                InventoryHandler.Instance.ConsumeItem(item);
                Destroy(gameObject);
                break;
            case Item.ItemUsages.Weapon:
                InventoryHandler.Instance.EquipItem(item);
                Destroy(gameObject);
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.ShowTooltip(item.ItemName,item.ItemDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip();
    }
}