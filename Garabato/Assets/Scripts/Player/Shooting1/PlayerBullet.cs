using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public float damage;

    void Start()
    {
     // Destroy(this, 2f);
    }
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //pasar damage a la vida de enemigo
        }
    }
    /*public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.collider.TryGetComponent(out EnemyLife enemyLife))
        {
            enemyLife.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);

    }*/
}
