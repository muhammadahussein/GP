using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementState : State
{
    public MovementState(Monster enemyAI) : base(enemyAI)
    {
        //EnemyAI.Agent.SetDestination(EnemyAI.transform.position);
    }

    public override IEnumerator OnStateEnter()
    {
        EnemyAI.Enemy.Run(true);
        EnemyAI.Enemy.Combat(false);
        yield break;
    }

    public override IEnumerator OnStateUpdate()
    {
        if (EnemyAI.isVisualizing)
            Debug.DrawRay(EnemyAI.transform.position, Vector3.up*2, Color.blue);

        Vector3 randomPos = Random.onUnitSphere;
        randomPos.y = 0;
        randomPos = randomPos.normalized * EnemyAI.PlayerDist/1.5f;
        EnemyAI.Agent.SetDestination(EnemyAI.PlayerPos+ randomPos);

        if (EnemyAI.PlayerDist <= Random.Range(3, 5) && !simpleControl.Instance.player.isRunning) 
            EnemyAI.SetState(new CombatState(EnemyAI));
        if (EnemyAI.Agent.remainingDistance > EnemyAI.DetectionRadius)
        {
            //EnemyAI.CanSeePlayer = false;
            //if (!EnemyAI.GroupSeePlayer)
            //{
            //    EnemyAI.TargetPos = EnemyAI.PlayerPos;
            //    EnemyAI.SetState(new SearchState(EnemyAI));
            //}
        }
        yield break;
    }
    public override IEnumerator PlayStateRoutine()
    {
        yield break;
    }
    public override void OnStateExit()
    {
        
    }
}
