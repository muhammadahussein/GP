using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour, IWeapon
{
    public List<Stat> Stats { get; set; }

    public void WeaponLightAttack()
    {
        Debug.Log("Light Attack");
    }
    private void OnTriggerEnter(Collider other)
    {
        IEnemy hitEnemy = other.GetComponent<IEnemy>();
        
        if (hitEnemy != null)
        {
            Debug.LogWarning(Stats[0].StatName);
            Stat damageStat = Stats.Find(x => x.StatName == "Damage");
            hitEnemy.TakeDamage(damageStat.StatValue);
            /*GameObject hitVFX = GameObject.Instantiate(Resources.Load<GameObject>("Hit VFX"));
            hitVFX.transform.position = transform.position + transform.forward;
            Destroy(hitVFX, 1.5f);*/

            //hitVFX.transform.position = other.
            /*RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                GameObject hitVFX = GameObject.Instantiate(Resources.Load<GameObject>("HitVFX"));
                hitVFX.transform.position = hit.point;
                Destroy(hitVFX, 1.5f);
            }*/
        }
    }
}
