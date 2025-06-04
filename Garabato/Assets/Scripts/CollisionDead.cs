using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDead : MonoBehaviour
{
    public bool isActive = true;
    private GameObject weapon;

    private void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return;
        if (collision.CompareTag("Box") && !GameManager.Instance.isMapActive)
        {
            Animator boxanimator = collision.GetComponent<Animator>();
            boxanimator.SetBool("isBreak", true);
            StartCoroutine(ReloadAfterTheAnimation(1f));
        }
    }

    private IEnumerator ReloadAfterTheAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
