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
        BoxCollider2D box = enemy.parent.GetComponent<BoxCollider2D>();
        if (box == null) return;

        // Obtener tamaño mundial del box
        Vector2 worldSize = new Vector2(
            box.size.x * enemy.parent.lossyScale.x,
            box.size.y * enemy.parent.lossyScale.y
        );

        // Obtener centro del box en mundo
        Vector2 boxCenter = (Vector2)enemy.parent.position + box.offset * enemy.parent.lossyScale;

        // Elegir punto aleatorio dentro del box
        Vector2 randomPoint = new Vector2(
            Random.Range(-worldSize.x / 2, worldSize.x / 2),
            Random.Range(-worldSize.y / 2, worldSize.y / 2)
        );

        targetPosition = boxCenter + randomPoint;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obtacle"))
        {
            SetNewTargetPosition();
        }
    }
}