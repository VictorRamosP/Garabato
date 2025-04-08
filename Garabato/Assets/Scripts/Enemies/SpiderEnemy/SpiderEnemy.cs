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

    [Header("Position")]
    public bool isUpsideDown;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new SpiderPatrolState(this, stateMachine, EdgedetectionPoint));
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeCam.isMapActive) return;
        stateMachine.OnUpdate();
    }
}
