using UnityEngine;

public class PatrolState : IState
{
    private BatEnemy enemy;
    private StateMachine stateMachine;
    private Vector2 targetPosition;
    public Vector2 TargetPosition => targetPosition;

    public PatrolState(BatEnemy enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        enemy.Map.OnMapRotated += MapRotated;
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

    void MapRotated()
    {
        SetNewTargetPosition();
    }
    void SetNewTargetPosition()
{
    BoxCollider2D box = enemy.parent.GetComponent<BoxCollider2D>();
    if (box == null) return;

    // Obtener un punto aleatorio dentro del box en coordenadas locales
    Vector2 localPoint = new Vector2(
        Random.Range(-0.5f, 0.5f) * box.size.x,
        Random.Range(-0.5f, 0.5f) * box.size.y
    );

    // Sumar el offset local del collider
    localPoint += box.offset;

    // Convertir ese punto local en global teniendo en cuenta rotación, posición y escala
    Vector2 worldPoint = enemy.parent.TransformPoint(localPoint);

    targetPosition = worldPoint;
}

    public void SetNewTargetFromCollision()
    {
        SetNewTargetPosition();
    }
}