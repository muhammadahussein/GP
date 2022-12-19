using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void TakeDamage(float damageAmount);
    void EnemyAttack();
    int EnemyID { get; set; }
}
