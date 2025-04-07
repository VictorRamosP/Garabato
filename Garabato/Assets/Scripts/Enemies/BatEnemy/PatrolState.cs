using UnityEngine;

public class PatrolState : IState
{
    private BatEnemy enemy;
    private StateMachine stateMachine;
    private bool isWaiting = false;
    private float waitCounter = 0f;
    private float waitTime;
    private Vector3 targetPosition;
    private float stuckTimer = 0f;
    private const float stuckThreshold = 5f;

    public PatrolState(BatEnemy enemy, StateMachine stateMachine, float waitTime)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.waitTime = waitTime;
        SetNewTarget();
    }

    public void OnEnter() { }
    public void OnExit() { }

    public void OnUpdate()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.position) <= enemy.ChaseDistance)
        {
            stateMachine.ChangeState(new ChaseState(enemy, stateMachine));
            return;
        }
        if (isWaiting)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0f)
            {
                isWaiting = false;
                SetNewTarget();
            }
        }
        else
        {
            Vector3 dir = (targetPosition - enemy.transform.position).normalized;
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
                SetNewTarget();
                stuckTimer = 0f;
            }
            if (Vector3.Distance(enemy.transform.position, targetPosition) < 0.1f)
            {
                isWaiting = true;
                waitCounter = waitTime;
            }
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckThreshold)
            {
                SetNewTarget();
                stuckTimer = 0f;
            }
        }
    }

    private void SetNewTarget()
    {
        float offsetX = Random.Range(-enemy.PatrolRadius, enemy.PatrolRadius);
        float offsetY = Random.Range(-enemy.PatrolRadius, enemy.PatrolRadius);
        targetPosition = enemy.PatrolCenter + new Vector3(offsetX, offsetY, 0);
    }
}
