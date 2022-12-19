using System.Collections;
using UnityEngine;

public class AttackState : State
{
    public AttackState(Monster enemyAI) : base(enemyAI)
    {
    }

    public override IEnumerator OnStateEnter()
    {
        EnemyAI.PlayStateRoutine();
        yield break;
    }
    public override IEnumerator OnStateUpdate()
    {
        if (EnemyAI.isVisualizing)
            Debug.DrawRay(EnemyAI.transform.position, Vector3.up * 2, Color.red);

        EnemyAI.Agent.SetDestination(EnemyAI.PlayerPos);
        if (EnemyAI.PlayerDist > 3.5f)
        {
            EnemyAI.SetState(new CombatState(EnemyAI));
        }
        yield break;
    }
    public override IEnumerator PlayStateRoutine()
    {
        EnemyAI.EnemyAttack();
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        EnemyAI.SetState(new CombatState(EnemyAI));
        //yield break;
    }
    public override void OnStateExit()
    {
        EnemyAI.StopStateRoutine();
    }
}
