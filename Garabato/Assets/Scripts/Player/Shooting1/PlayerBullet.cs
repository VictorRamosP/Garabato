using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public AudioClip impactSound;
    private AudioSource _audioSource;

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }
    void Update()
    {
        if (GameManager.Instance.isMapActive) return;

        transform.Translate(Vector2.up * speed * Time.deltaTime);
        _audioSource = GetComponent<AudioSource>();
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (playerCamera != null)
        {
            Vector3 viewportPos = playerCamera.WorldToViewportPoint(transform.position);

            if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
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
        else if (collision.CompareTag("Breakable"))
        {
            collision.GetComponent<LifeWall>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
   
}
