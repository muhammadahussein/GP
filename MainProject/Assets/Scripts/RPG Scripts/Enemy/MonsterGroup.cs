using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroup : StateMachine
{
    Monster[] Monsters;
    bool GroupSeePlayer;

    void Awake()
    {
        Monsters = GetComponentsInChildren<Monster>();
    }

    void FixedUpdate()
    {
        int seeCount = 0;
        foreach (Monster monster in Monsters)
        {
            if (monster.CanSeePlayer)
            {
                seeCount++;
            }
        }
        if (seeCount > 0)
            GroupSeePlayer = true;
        else
            GroupSeePlayer = false;

        foreach (Monster monster in Monsters)
        {
            monster.CanSeePlayer = GroupSeePlayer;
        }
    }
}
