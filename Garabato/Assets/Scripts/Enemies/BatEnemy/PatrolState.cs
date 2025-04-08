using UnityEngine;

public class PatrolState : IState
{
    private BatEnemy enemy;
    private StateMachine stateMachine;
    private Vector2 targetPosition;
    

    public PatrolState(BatEnemy enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        enemy.Map.OnMapRotated += SetNewTargetPosition;
    }

    public void OnEnter()
    {
        SetNewTargetPosition();
    }

    public void OnExit() { }

    public void OnUpdate()
{
    if (enemy.CanSeePlayer())
    {
        stateMachine.ChangeState(new ChaseState(enemy, stateMachine));
        return;
    }

    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPosition, enemy.MoveSpeed * Time.deltaTime);

    if (Vector2.Distance(enemy.transform.position, targetPosition) < 0.05f)
    {
        SetNewTargetPosition();
    }
}


    void SetNewTargetPosition()
    {
        CircleCollider2D circle = enemy.parent.GetComponent<CircleCollider2D>();
        if (circle == null) return;

        float worldRadius = circle.radius * enemy.parent.lossyScale.x;
        Vector2 randomPoint = Random.insideUnitCircle * worldRadius;
        targetPosition = (Vector2)enemy.parent.position + randomPoint;
    }
}