using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeareaHandler : MonoBehaviour
{
    public enum Direction {up, down, left, right};
    [SerializeField] public Direction AreaDirection;
    

    [HideInInspector]
    public bool IsPlayerInSpawnpoint;

    [HideInInspector]
    public List<GameObject> Spawners = new List<GameObject>();
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("SpikeSpawner"))
            {
                Spawners.Add(child.gameObject);
                child.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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
