using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour , IWeapon
{
    public List<Stat> Stats { get; set; }

    public void WeaponLightAttack()
    {
        Debug.Log("Light Attack");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Stat damageStat = Stats.Find(x => x.StatName == "Damage");
            other.GetComponent<IEnemy>().TakeDamage(damageStat.StatValue);
        }
    }
}
