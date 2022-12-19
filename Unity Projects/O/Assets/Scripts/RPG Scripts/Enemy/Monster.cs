using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IEnemy
{
    public float maxHealth, damage;
    private float health;

    private void Start()
    {
        health = maxHealth;
    }
    public void EnemyAttack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0) Die();
    }
    void Die()
    {
        Destroy(gameObject);
    }

}
