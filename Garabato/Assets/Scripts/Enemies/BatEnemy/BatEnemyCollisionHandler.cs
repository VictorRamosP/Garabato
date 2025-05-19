using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemyCollisionHandler : MonoBehaviour
{
    public event Action<Collider2D> BatCollided;
    void OnTriggerEnter2D(Collider2D collision)
    {
        BatCollided?.Invoke(collision);
    }
}
