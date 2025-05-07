using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer color;

    private void Start()
    {
        animator = GetComponent<Animator>();
        color = GetComponent<SpriteRenderer>();
        color.color = Color.red;
    }

    public void Open()
    {
        this.gameObject.SetActive(false);
        //animator.SetBool("IsOpen", true);
    }

    public void Close()
    {
        color.color = Color.red;
        //this.gameObject.SetActive(true);

        //animator.SetBool("IsOpen", false);
    }
}
