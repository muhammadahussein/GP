using System.Collections;
using UnityEngine;

public class CombatState : State
{
    public CombatState(Monster enemyAI) : base(enemyAI)
    {
    }

    public override IEnumerator OnStateEnter()
    {
        EnemyAI.Enemy.Combat(true);
        //int randomX = Random.Range(-1,2);
        //EnemyAI.Enemy.setAxis(new Vector2(1, 0));
        yield break;
    }
    public override IEnumerator OnStateUpdate()
    {
        if (EnemyAI.isVisualizing)
            Debug.DrawRay(EnemyAI.transform.position, Vector3.up * 2, Color.yellow);

        EnemyAI.Agent.SetDestination(EnemyAI.PlayerPos);

        if (EnemyAI.PlayerDist > Random.Range(5, 7))
            EnemyAI.SetState(new MovementState(EnemyAI));

        if (EnemyAI.PlayerDist < 3f)
        {
            EnemyAI.SetState(new AttackState(EnemyAI));
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
