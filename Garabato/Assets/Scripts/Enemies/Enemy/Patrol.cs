using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float patrolspeed;
    private float disFloorDetection = 2;
    private float disObstacleDetection = 2;

    private bool infFloor;
    private bool infObstacles;
    private Rigidbody2D _rb;
    public Transform contrfloor;
    public Transform contrObst;
    public LayerMask layerFloor;
    public LayerMask layerobstacles;
    private bool mirandoder = true;
    public EnemyLife Life;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Life.OnDeath += Freeze;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isMapActive || Life.isDead || GameManager.Instance.isCameraReturning) 
        {
            Freeze();
            return;
        }
        if (_rb.constraints == RigidbodyConstraints2D.FreezeAll)
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.isKinematic = false;
        }
        Patrolfunc();

    }
    public void Patrolfunc()
    {
        _rb.velocity = new Vector2(mirandoder ? patrolspeed : -patrolspeed, _rb.velocity.y);

        infObstacles = Physics2D.Raycast(contrObst.position, transform.right, disObstacleDetection, layerobstacles);
        infFloor = Physics2D.Raycast(contrfloor.position, Vector2.down, disFloorDetection, layerFloor);

        if (infObstacles || !infFloor)
        {
            Girar(ref mirandoder);
        }
    }

    private void Girar(ref bool mirandoder)
    {
        mirandoder = !mirandoder;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }
    void Freeze()
    {
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _rb.isKinematic = true;
    }
}
