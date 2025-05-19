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

    [Header("Scripts de funciones")]
    public BatPatrolArea Area;
    public GameObject Bat;
    void Start()
    {
        Map = GameObject.FindAnyObjectByType<RotateMap>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        Area.SomethingInArea += SomethingInArea;
        Area.SomethingLeftArea += SomethingLeftArea;
        Map.OnMapRotated += OnMapRotated;

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new PatrolState(this, stateMachine));
    }

    void Update()
    {
        if (ChangeCam.isMapActive) return;

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

    void OnMapRotated()
    {
        Bat.transform.rotation = Quaternion.identity;
    }
}
