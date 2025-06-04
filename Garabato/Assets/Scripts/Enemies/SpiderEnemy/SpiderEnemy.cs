using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    private StateMachine stateMachine;
    public int Speed;
    public LayerMask WhatIsGround;
    public float TongueSpeed;
    public float AttackVisionCooldown;

    [Header("Position")]
    public bool isUpsideDown;
    [Header("Componentes")]
    public Transform EdgedetectionPoint;
    public GameObject Tongue;
    public Transform TongueSpawner;

    [Header("Dependencias")]
    public RotateMap Map;
    public EnemyLife Life;
    public SpiderAttackZone spiderAttack;
    // Start is called before the first frame update
    void Start()
    {
        if (Map == null) Debug.LogError("Map no está asignado en SpiderEnemy.");

        //spiderAttack.OnPlayerEntered += PlayerInArea;

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new SpiderPatrolState(this, stateMachine, EdgedetectionPoint));
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeCam.isMapActive || Life.isDead || ChangeCam.isReturning) return;
        stateMachine.OnUpdate();
    }

    void PlayerInArea(Collider2D player)
    {
        spiderAttack.isActive = false;
        stateMachine.ChangeState(new SpiderAttackState(this, stateMachine, player.transform));
    }
    public IEnumerator VisionCooldown()
    {
        yield return new WaitForSeconds(AttackVisionCooldown);
        spiderAttack.isActive = true;
        yield return null;
    }
}
