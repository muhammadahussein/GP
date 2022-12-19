using System.Collections;

public abstract class State
{
    protected Monster EnemyAI;

    public State(Monster enemyAI)
    {
        EnemyAI = enemyAI;
    }

    public virtual IEnumerator OnStateEnter()
    {
        yield break;
    }
    public virtual IEnumerator OnStateUpdate()
    {
        yield break;
    }
    public virtual IEnumerator PlayStateRoutine()
    {
        yield break;
    }
    public virtual void OnStateExit()
    {
    }
}
