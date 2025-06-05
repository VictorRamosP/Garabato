using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackZone : MonoBehaviour
{
    //public event Action<Collider2D> OnPlayerEntered;
    //public event Action OnPlayerExited;
    public bool isActive;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            //OnPlayerEntered?.Invoke(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            //OnPlayerExited?.Invoke();
        }
    }
}
