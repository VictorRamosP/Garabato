using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeWall : MonoBehaviour
{
    public float health = 200f;

    private float initialHealth;
    private Animator animator;
    private bool isDead = false;
 
    public AudioClip glassBreakSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        initialHealth = health;

    }

    public void TakeDamage(float damageTaken)
    {
        if (isDead) return; 

        health -= damageTaken;
        if (health <= initialHealth/2 )
        {
            Debug.Log("entra");
            animator.SetBool("isLowHealth", true);
        }

        if (health <= 0)
        {
            isDead = true;
            if (animator != null)
            {
                animator.SetBool("isBreak", true);
                StartCoroutine(DisableAfterDeath());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator DisableAfterDeath()
    {
        if (glassBreakSound != null)
        {
            AudioSource.PlayClipAtPoint(glassBreakSound, transform.position);
        }
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); 
        
        gameObject.SetActive(false);
    }
}
