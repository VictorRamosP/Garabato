using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform LeftPoint;
    public Transform RightPoint;
    public int Speed;
    private Transform Destination;
    // Start is called before the first frame update
    void Start()
    {
        Destination = RightPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeCam.isMapActive) return;
        
        if (DestinationReached()) SetNewDestination();
        transform.position = Vector3.MoveTowards(transform.position, Destination.position, Time.deltaTime * Speed);
    }

    void SetNewDestination()
    {
        if (Destination == LeftPoint) Destination = RightPoint;
        else Destination = LeftPoint;
    }
    bool DestinationReached()
    {
        bool hasReached = false;

        float distance = Vector3.Distance(transform.position, Destination.position);
        if (distance <= 0.5f) hasReached = true;

        return hasReached;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}
