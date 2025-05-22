using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpHole : MonoBehaviour
{
    public TpHole TpTo;
    [HideInInspector] public bool isTpActive;
    void Start()
    {
        isTpActive = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTpActive)
        {
            isTpActive = false;
            TpTo.isTpActive = false;
            collision.transform.position = TpTo.transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTpActive = true;
        }
    }
}
