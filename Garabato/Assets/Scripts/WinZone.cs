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
    IEnumerator LoadLevel(string level)
    {
        GameObject.FindAnyObjectByType<ChangeCam>().Change();
        GameObject.Find("MapAnimation").GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (finishcooldawn)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                StartCoroutine(LoadLevel(LevelToLoad));
            }
        }
    }
}
