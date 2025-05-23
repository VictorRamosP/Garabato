using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float health = 100f;
    private Animator animator;
    public bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageTaken)
    {
        if (isDead) return;

        health -= damageTaken;
        if (health <= 0)
        {
            isDead = true;

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != null && script != this) 
                script.enabled = false;
        }
         Collider2D[] colliders = GetComponents<Collider2D>();
         foreach (Collider2D col in colliders)
         {
                if (col != null && col.isTrigger)
                {
                    col.enabled = false;
                }
            }
           
            this.enabled = false;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
        
    }
}
