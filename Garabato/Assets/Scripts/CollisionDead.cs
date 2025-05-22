using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDead : MonoBehaviour
{
    private bool isDead = false;
    public bool isActive = true;
    private GameObject weapon;

    private void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return;
        if (!isDead && collision.CompareTag("Player") && !ChangeCam.isMapActive)
        {
            isDead = true;

            if (weapon != null)
            {
                weapon.SetActive(false);
            }
            Animator playerAnimator = collision.GetComponent<Animator>();
             if (playerAnimator != null)
             {
                 playerAnimator.SetTrigger("Die"); 
             }

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.bodyType = RigidbodyType2D.Kinematic; 
            }

            // Desactiva el control del jugador
            MonoBehaviour[] scripts = collision.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script != this) 
                    script.enabled = false;
            }

            
            StartCoroutine(ReloadAfterTheAnimation(1f)); // Poner Duracion Animacion

        }
        if (collision.CompareTag("Box") && !ChangeCam.isMapActive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StartCoroutine(ReloadAfterTheAnimation(1f));
        }
    }

    private IEnumerator ReloadAfterTheAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
