using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !ChangeCam.isMapActive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
