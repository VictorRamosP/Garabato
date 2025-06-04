using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    public float coolDown;
    private float coolDownTimer;
    public string LevelToLoad;
    private bool finishcooldawn = false;

    private void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (coolDownTimer >= coolDown)
        {
            finishcooldawn = true;
        }
    }
    public void LoadLevel(string Level)
    {
        SceneManager.LoadScene(Level);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (finishcooldawn)
        {
            if (collision.CompareTag("Player"))
            {
                LoadLevel(LevelToLoad);
            }
        }
    }
}
