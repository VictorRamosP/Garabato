using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    private StateMachine stateMachine;

    [Header("Variables")]
    public float MoveSpeed = 5f;
    public float ChaseSpeed = 3f;
    public float NewTargetCooldown = 0f;

    [HideInInspector]
    public RotateMap Map;
    public Transform Player;
    public LayerMask ObstacleLayer;
    public EnemyLife Life;

    [Header("Scripts de funciones")]
    public BatPatrolArea Area;
    public GameObject Bat;
    void Start()
    {
        Map = GameObject.FindAnyObjectByType<RotateMap>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        Area.SomethingInArea += SomethingInArea;
        Area.SomethingLeftArea += SomethingLeftArea;

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new PatrolState(this, stateMachine));
    }

    void Update()
    {
        Bat.transform.rotation = Quaternion.identity;
        if (GameManager.Instance.isMapActive || Life.isDead || GameManager.Instance.isCameraReturning) return;
        
        stateMachine.OnUpdate();
    }

    void SomethingInArea(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stateMachine.ChangeState(new ChaseState(this, stateMachine));
        }
    }

    void SomethingLeftArea(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stateMachine.ChangeState(new PatrolState(this, stateMachine));
        }
    }
}
