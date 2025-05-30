using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    private Vector3 spawnPoint;
    private Vector3 player;
    private float speed;
    public bool isComingBack;
    public bool isDead;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player) < 0.01f && !isComingBack) isComingBack = true;
        if (isComingBack && Vector3.Distance(transform.position, spawnPoint) < 0.01f)
        {
            Destroy(this.gameObject);   
        }
        if (spawnPoint != null && player != null)
        {
            if (!isComingBack) transform.position = Vector3.MoveTowards(transform.position, player, speed * Time.deltaTime);
            else transform.position = Vector3.MoveTowards(transform.position, spawnPoint, speed * Time.deltaTime);
        }
    }

    public void Fire(Vector3 _spawn, Vector3 _player, float _speed)
    {
        isComingBack = false;
        spawnPoint = _spawn;
        player = _player;
        speed = _speed;
        isDead = false;
    }
    public void UpdateInfo(Vector3 _spawn)
    {
        spawnPoint = _spawn;
    }
    public void Kill()
    {
        Destroy(this.gameObject);
        speed = 0;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SpiderAttackZone"))
        {
            isComingBack = true;
        }   
    }
}
