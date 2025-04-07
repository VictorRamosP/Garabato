using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public float PatrolRadius = 10f;
    public float ChaseDistance = 5f;
    public float MoveSpeed = 2f;
    public LayerMask ObstacleLayer;
    public Transform Player;
    public Rigidbody2D Rigidbody;
    public Vector3 PatrolCenter { get; private set; }
    private StateMachine stateMachine;

    void Start()
    {
        PatrolCenter = transform.position;
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new PatrolState(this, stateMachine, 2f));
    }

    void Update()
    {
        stateMachine.OnUpdate();
    }

    public Vector3 ClampPositionToArea(Vector3 position)
    {
        float clampedX = Mathf.Clamp(position.x, PatrolCenter.x - PatrolRadius, PatrolCenter.x + PatrolRadius);
        float clampedY = Mathf.Clamp(position.y, PatrolCenter.y - PatrolRadius, PatrolCenter.y + PatrolRadius);
        return new Vector3(clampedX, clampedY, position.z);
    }
}
