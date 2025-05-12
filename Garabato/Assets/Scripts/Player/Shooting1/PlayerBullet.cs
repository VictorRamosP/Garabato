using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public AudioClip impactSound;
    private AudioSource _audioSource;


    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Breakable"))
        {
            if (impactSound != null)
            {
                AudioSource.PlayClipAtPoint(impactSound, transform.position, 0.8f);
            }
            collision.GetComponent<EnemyLife>().TakeDamage(damage);
            Destroy(this.gameObject);            
        }
        else if (collision.CompareTag("Obstacle")) {
            Destroy(this.gameObject);
        }
    }
   
}
