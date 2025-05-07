using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPatrolState : IState
{
    private SpiderEnemy enemy;
    private StateMachine stateMachine;
    private Transform EdgedetectionPoint;
    private bool isRotated;
    public SpiderPatrolState(SpiderEnemy enemy, StateMachine stateMachine, Transform EdgedetectionPoint)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.EdgedetectionPoint = EdgedetectionPoint;
        this.isRotated = false;
    }
    public void OnEnter()
    {
        if (enemy.Map == null)
        {
            Debug.LogError("Map no está asignado en el SpiderEnemy.");
            return;
        }

        enemy.Map.OnMapRotated += MapRotated;
    }
    public void OnUpdate()
    {
        if (EdgeDetected() || WallDetected()) {
            Flip();
        }
        Move();
    }
    public void OnExit()
    {
        //Debug.Log("Spider exiting patrolling State.");
    }
    private void Move()
    {
        enemy.transform.Translate(enemy.transform.right * enemy.Speed * Time.deltaTime, Space.World);
    }
    private bool EdgeDetected()
    {

        RaycastHit2D hit = Physics2D.Raycast(EdgedetectionPoint.position, (-enemy.transform.up), 1.5f, enemy.WhatIsGround);

        //Gizmos
        Debug.DrawLine(EdgedetectionPoint.position, EdgedetectionPoint.position + (-enemy.transform.up) * 1.5f, Color.green); 

        return (hit.collider == null);
    }

    private bool WallDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgedetectionPoint.position, enemy.transform.right, 0.5f, enemy.WhatIsGround);

        Debug.DrawLine(EdgedetectionPoint.position, EdgedetectionPoint.position + enemy.transform.right * 0.5f, Color.red);
        return (hit.collider != null);
    }
    private void Flip()
    {
        enemy.transform.Rotate(0, 180, 0);
    }

    private void MapRotated()
    {
        isRotated = !isRotated;
    }
}

