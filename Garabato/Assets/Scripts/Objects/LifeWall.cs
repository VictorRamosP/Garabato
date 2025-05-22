using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeWall : MonoBehaviour
{
    public float health = 200f;
    public Sprite lowHealthSprite; 

    private SpriteRenderer spriteRenderer;
    private bool hasChangedSprite = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;

        if (health <= 100f && !hasChangedSprite)
        {
            if (lowHealthSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = lowHealthSprite;
                hasChangedSprite = true;
            }
        }

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

