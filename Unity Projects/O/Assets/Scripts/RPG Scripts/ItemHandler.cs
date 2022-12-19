using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public void ConsumeItem(Item item)
    {
        /*will be changed to consuming effect or something*/ GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Consumable Items/" + item.ItemName));
        spawnedItem.GetComponent<IConsumable>().Consume();
        Tooltip.HideTooltip();
    }
}

public interface IConsumable
{
    void Consume();
    //override when the item is used to modify stats
    //void Consume(CharacterStats charStats);
}
