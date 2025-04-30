using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public GameObject flame;
    private float cooldownTimer = 0f;
    public float reactiveFlame = 3f;
    public float desactiveFlame = 5f;
    void Start()
    {
        flame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= reactiveFlame)
        {
            flame.SetActive(true);
        }
        if (cooldownTimer >= desactiveFlame)  
        {
            flame.SetActive(false);
            cooldownTimer = 0f;
        }
    }
}
