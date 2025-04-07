using UnityEngine;

public class ChaseState : IState
{
    private BatEnemy enemy;
    private StateMachine stateMachine;

    public ChaseState(BatEnemy enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public void OnEnter() { }

    public void OnExit() { }

   public void OnUpdate()
{
    if (!enemy.CanSeePlayer())
    {
        stateMachine.ChangeState(new PatrolState(enemy, stateMachine));
        return;
    }

    Vector2 direction = (enemy.Player.position - enemy.transform.position).normalized;
    Vector2 newPos = (Vector2)enemy.transform.position + direction * enemy.ChaseSpeed * Time.deltaTime;

    RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction, 0.5f, enemy.ObstacleLayer);
    if (hit.collider == null)
    {
        enemy.transform.position = newPos;
    }
    else
    {
        stateMachine.ChangeState(new PatrolState(enemy, stateMachine));
    }
}

}
