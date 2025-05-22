using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100f;
   
    public void TakeDamage(float damageTaken) {
        health -= damageTaken;
        if (health <= 0) {
            gameObject.SetActive(false);
        }
    }
}
