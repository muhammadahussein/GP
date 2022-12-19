using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Transform WeaponPos { get; set; }
    [HideInInspector] public GameObject Equipped;
    private CharacterStats charStats = null;
    private Item currentWeapon;

    private void Awake()
    {
        charStats = CharacterStats.Instance;
    }
    public void Equip(Item item)
    {
        if (Equipped != null)
            Unequip();
        //https://docs.unity3d.com/ScriptReference/Resources.Load.html
        Equipped = Resources.Load<GameObject>("Weapons/" + item.ItemName);
        simpleControl.Instance.player.Wield(ref Equipped);
        currentWeapon = item;
        Equipped.GetComponent<IWeapon>().Stats = item.Stats;
        charStats.AddStatAdditive(item.Stats);
        StatsUI.Instance.UpdateStatsUI();
        Tooltip.HideTooltip();
    }
    private void Unequip()
    {
        charStats.RemoveStatAdditive(Equipped.GetComponent<IWeapon>().Stats);
        InventoryHandler.Instance.AddItem(currentWeapon.ItemName);
        //Destroy(Equipped.gameObject);
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


