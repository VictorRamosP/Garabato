using UnityEngine;

public class ChaseState : IState
{
    private BatEnemy enemy;
    private StateMachine stateMachine;
    private float stuckTimer = 0f;
    private const float stuckThreshold = 5f;

    public ChaseState(BatEnemy enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public void OnEnter() { }
    public void OnExit() { }

    public void OnUpdate()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.position) > enemy.ChaseDistance)
        {
            stateMachine.ChangeState(new PatrolState(enemy, stateMachine, 2f));
            return;
        }
        Vector3 dir = (enemy.Player.position - enemy.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, dir, 0.5f, enemy.ObstacleLayer);
        if (hit.collider == null)
        {
            Vector3 newPos = enemy.transform.position + dir * enemy.MoveSpeed * Time.deltaTime;
            newPos = enemy.ClampPositionToArea(newPos);
            enemy.Rigidbody.MovePosition(newPos);
            stuckTimer = 0f;
        }
        else
        {
            SetRandomDirection();
            stuckTimer = 0f;
        }
        stuckTimer += Time.deltaTime;
        if (stuckTimer >= stuckThreshold)
        {
            SetRandomDirection();
            stuckTimer = 0f;
        }
    }

    private void SetRandomDirection()
    {
        Vector3 baseDir = (enemy.Player.position - enemy.transform.position).normalized;
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.2f, 1f), 0);
        Vector3 result = (baseDir + randomOffset).normalized;
        if (result.y < -0.5f)
        {
            result.y = -0.5f;
            result = result.normalized;
        }
        enemy.Rigidbody.MovePosition(enemy.transform.position + result * enemy.MoveSpeed * Time.deltaTime);
    }
}
