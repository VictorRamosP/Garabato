using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    private StateMachine stateMachine;
    public int Speed;
    public Transform EdgedetectionPoint;
    public LayerMask WhatIsGround;
    public RotateMap Map;
    public Rigidbody2D rb;
    public EnemyLife Life;

    [Header("Position")]
    public bool isUpsideDown;
    // Start is called before the first frame update
    void Start()
    {
        if (Map != null)
        {
           //Debug.Log("El Map de SpiderEnemy es: " + Map.name);
        }
        else
        {
            Debug.LogError("Map no está asignado en SpiderEnemy.");
        }
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new SpiderPatrolState(this, stateMachine, EdgedetectionPoint));
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeCam.isMapActive || Life.isDead) return;
        stateMachine.OnUpdate();
    }
}
