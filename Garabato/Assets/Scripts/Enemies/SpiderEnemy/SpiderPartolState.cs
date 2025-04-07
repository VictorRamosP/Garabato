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
        enemy.Map.OnMapRotated += MapRotated;
    }
    public void OnEnter()
    {
        Debug.Log("Spider entering patrolling State.");
    }
    public void OnUpdate()
    {
        if (EdgeDetected() || WallDetected()) Flip();

        Move();
    }
    public void OnExit()
    {
        Debug.Log("Spider exiting patrolling State.");
    }
    private void Move()
    {
        enemy.transform.Translate(enemy.transform.right * enemy.Speed * Time.deltaTime, Space.World);
    }
    private bool EdgeDetected()
    {
        Vector3 direction = Vector3.down;
        switch (enemy.Map.WhereIsDown)
        {
            case "up":
                direction = Vector3.up;
                break;
            case "down":
                direction = Vector3.down;
                break;
            case "right":
                direction = Vector3.right;
                break;
            case "left":
                direction = Vector3.left;
                break;
        }
        if (enemy.isUpsideDown)
        {
            direction = -direction;
        }
        RaycastHit2D hit = Physics2D.Raycast(EdgedetectionPoint.position, direction, 1.5f, enemy.WhatIsGround);

        //Gizmos
        Debug.DrawLine(EdgedetectionPoint.position, EdgedetectionPoint.position + direction * 1.5f, Color.green); 

        return (hit.collider == null);
    }

    private bool WallDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgedetectionPoint.position, Vector2.right, 1.5f, enemy.WhatIsGround);

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

