using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackState : IState
{
    private SpiderEnemy enemy;
    private StateMachine stateMachine;
    private Transform PlayerPosition;
    public GameObject tongue;
    public SpiderAttackState(SpiderEnemy enemy, StateMachine stateMachine, Transform playerPosition)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.PlayerPosition = playerPosition;
    }
    public void OnEnter()
    {
        tongue = GameObject.Instantiate(enemy.Tongue, enemy.TongueSpawner);
        Vector2 direction = enemy.TongueSpawner.position - PlayerPosition.position;

        direction.Normalize();

        tongue.GetComponent<TongueScript>().Fire(enemy.TongueSpawner.position, PlayerPosition.position, enemy.TongueSpeed);
    }
    public void OnUpdate()
    {
        if (enemy.Life.isDead) tongue.GetComponent<TongueScript>().Kill();
        if (Vector3.Distance(tongue.transform.position, enemy.TongueSpawner.position) < 0.01f && tongue.GetComponent<TongueScript>().isComingBack)
        {
            stateMachine.ChangeState(new SpiderPatrolState(enemy, stateMachine, enemy.EdgedetectionPoint));
            GameObject.Destroy(tongue);
            //enemy.spiderAttack.isActive = true;
        }
    }

    public void OnExit()
    {
        enemy.StartCoroutine(enemy.VisionCooldown());
    }
}


