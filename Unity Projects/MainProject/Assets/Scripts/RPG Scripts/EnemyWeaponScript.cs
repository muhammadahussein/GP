using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponScript : MonoBehaviour
{
    [SerializeField] float weaponDamage = 20f;

    private void OnTriggerEnter(Collider other)
    {
        simpleControl hitPlayer = other.GetComponent<simpleControl>();

        if (hitPlayer != null)
        {
            hitPlayer.TakeDamage(weaponDamage);
        }
    }
}
