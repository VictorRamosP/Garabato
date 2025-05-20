using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public Vector3 Spawnpoint;

    [HideInInspector]
    public bool IsPlayerInSpawnpoint;
    void Start()
    {
        Spawnpoint = transform.position;
    }

    void Update()
    {
        Spawnpoint = transform.position;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInSpawnpoint = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInSpawnpoint = false;
        }
    }
}
