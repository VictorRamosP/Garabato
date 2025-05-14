using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlate : MonoBehaviour
{
    // Start is called before the first frame update
    public bool keepOpen = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var spike in GameObject.FindObjectsOfType<CollisionDead>()) {
                spike.isActive = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !keepOpen)
        {
            foreach (var spike in GameObject.FindObjectsOfType<CollisionDead>()) {
                spike.isActive = true;
            }
        }
    }
}
