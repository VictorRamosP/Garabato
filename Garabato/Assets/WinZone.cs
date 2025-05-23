using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    public string LevelToLoad;
    public void LoadLevel(string Level)
    {
        SceneManager.LoadScene(Level);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadLevel(LevelToLoad);
        }
    }
}
