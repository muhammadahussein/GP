using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject hand;
    public GameObject Equipped { get; set; }
    private CharacterStats charStats = null;
    private void Awake()
    {
        charStats = CharacterStats.Instance;
    }
    public void Equip(Item item)
    {
        if (Equipped != null)
            Unequip();
        //https://docs.unity3d.com/ScriptReference/Resources.Load.html
        Equipped = Instantiate(Resources.Load<GameObject>("Weapons/" + item.ItemName), hand.transform.position, hand.transform.rotation);
        Equipped.GetComponent<IWeapon>().Stats = item.Stats;
        Equipped.transform.SetParent(hand.transform);
        charStats.AddStatAdditive(item.Stats);
        StatsUI.Instance.UpdateStatsUI();
        Tooltip.HideTooltip();
    }
    private void Unequip()
    {
        charStats.RemoveStatAdditive(Equipped.GetComponent<IWeapon>().Stats);
        string equippedItemName = Equipped.GetComponent<IWeapon>().GetType().ToString();
        InventoryHandler.Instance.AddItem(equippedItemName);
        Destroy(Equipped.gameObject);
    }
    public void WeaponAttack()
    {
        Equipped.GetComponent<IWeapon>().WeaponLightAttack();
    }
}

public interface IWeapon
{
    List<Stat> Stats { get; set; }
    void WeaponLightAttack();
}


