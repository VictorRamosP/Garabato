using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public Transform parent;
    public float ChaseDistance = 5f;
    public float MoveSpeed = 5f;
    public float ChaseSpeed = 3f;
    public LayerMask ObstacleLayer;
    public Transform Player;
    public Rigidbody2D Rigidbody;
    private StateMachine stateMachine;

    void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new PatrolState(this, stateMachine));
    }

    void Update()
    {
        stateMachine.OnUpdate();
    }

    public bool CanSeePlayer()
    {
        if (Player == null) return false;

        Vector2 direction = (Player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, Player.position);

        if (distance > ChaseDistance) return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, ObstacleLayer);
        return hit.collider == null;
    }

}
