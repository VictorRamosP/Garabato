using UnityEngine;

public class PatrolState : IState
{
    private BatEnemy enemy;
    private StateMachine stateMachine;
    private Vector2 targetPosition;
    private float cooldown;
    public Vector2 TargetPosition => targetPosition;

    public PatrolState(BatEnemy enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;

        enemy.Map.OnMapRotated += MapRotated;
        //enemy.Bat.GetComponent<BatEnemyCollisionHandler>().BatCollided += BatCollisioned;
    }

    public void OnEnter()
    {
        targetPosition = enemy.Area.GetNewTargetPosition();
    }

    public void OnExit() { }

    public void OnUpdate()
    {
        enemy.Bat.transform.position = Vector2.MoveTowards(enemy.Bat.transform.position, targetPosition, enemy.MoveSpeed * Time.deltaTime);

        cooldown += Time.deltaTime;

        if (Vector2.Distance(enemy.Bat.transform.position, targetPosition) < 0.05f && cooldown >= enemy.NewTargetCooldown)
        {
            targetPosition = enemy.Area.GetNewTargetPosition();
            cooldown = 0f;
        }
    }

    void MapRotated()
    {
        targetPosition = enemy.Area.GetNewTargetPosition();
    }

    void BatCollisioned(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            targetPosition = enemy.Area.GetNewTargetPosition();
        }
    }
    public void SetNewTargetFromCollision()
    {
        targetPosition = enemy.Area.GetNewTargetPosition();
    }
}