using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        this.gameObject.SetActive(false);
        //animator.SetBool("IsOpen", true);
    }

    public void Close()
    {
        this.gameObject.SetActive(true);

        //animator.SetBool("IsOpen", false);
    }
}
