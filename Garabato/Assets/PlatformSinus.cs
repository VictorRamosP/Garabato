using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSinus : MonoBehaviour
{
    // Start is called before the first frame update
    public float amplitude = 0.5f;
    public float frequency = 1f;
    private float delay;
    private Vector3 startPos;
    void Start()
    {
        GameObject.FindObjectOfType<RotateMap>().OnMapRotated += OnMapRotated;
        startPos = transform.position;
        delay = Random.Range(0f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isMapActive) return;
        float y = Mathf.Sin((Time.time + delay) * frequency) * amplitude;
        transform.position = startPos + new Vector3(0, y, 0);
    }
    void OnMapRotated()
    {
        startPos = transform.position;
    }
}