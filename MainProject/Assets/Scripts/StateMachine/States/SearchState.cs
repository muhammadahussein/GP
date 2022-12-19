using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : State
{
    public SearchState(Monster enemyAI) : base(enemyAI)
    {
    }
    public override IEnumerator OnStateEnter()
    {
        EnemyAI.Enemy.Run(false);
        EnemyAI.PlayStateRoutine();
        yield break;
    }
    public override IEnumerator OnStateUpdate()
    {
        if (EnemyAI.isVisualizing)
            Debug.DrawRay(EnemyAI.transform.position, Vector3.up * 2, Color.green);

        Vector3 playerDir = (EnemyAI.PlayerPos - EnemyAI.transform.position).normalized;
        float dotProd = Vector3.Dot(playerDir, EnemyAI.transform.forward);
        if ((EnemyAI.PlayerDist < EnemyAI.DetectionRadius && dotProd > 0.25f) || EnemyAI.PlayerDist < 2)
        {
            Ray visionRay = new Ray(EnemyAI.transform.position + Vector3.up, playerDir);
            if (!Physics.Raycast(visionRay, EnemyAI.PlayerDist, EnemyAI.Enemy.rayLayerMasks))
            {
                EnemyAI.CanSeePlayer = true;
            }
        }
        if (EnemyAI.CanSeePlayer)
            EnemyAI.SetState(new MovementState(EnemyAI));

        //if(EnemyAI.GroupSeePlayer)
        //{
        //    EnemyAI.SetState(new MovementState(EnemyAI));
        //}
        yield break;
    }
    public override IEnumerator PlayStateRoutine()
    {
        while (true)
        {
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(EnemyAI.TargetPos, out navMeshHit, 10f, NavMesh.AllAreas);
            EnemyAI.TargetPos = navMeshHit.position;

            EnemyAI.Agent.SetDestination(EnemyAI.TargetPos);
            if (!EnemyAI.Enemy.isMoving)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 3));
                Vector3 randomTarget = EnemyAI.InitialPos + Random.onUnitSphere * 10f;
                EnemyAI.TargetPos = randomTarget;
            }
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    public override void OnStateExit()
    {
        EnemyAI.StopStateRoutine();
    }
}
