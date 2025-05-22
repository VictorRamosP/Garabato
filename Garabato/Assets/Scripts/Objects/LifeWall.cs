using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeWall : MonoBehaviour
{
    public float health = 200f;
    public Sprite lowHealthSprite;

    private SpriteRenderer spriteRenderer;
    private bool hasChangedSprite = false;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageTaken)
    {
        if (isDead) return; 

        health -= damageTaken;

        if (health <= 101f && !hasChangedSprite)
        {
            if (lowHealthSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = lowHealthSprite;
                hasChangedSprite = true;
            }
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
        yield return new WaitForSeconds(1f); 
        gameObject.SetActive(false);
    }
}
