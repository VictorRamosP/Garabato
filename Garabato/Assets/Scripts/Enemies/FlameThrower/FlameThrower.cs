using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public bool infinite;
    public GameObject flame;
    private float cooldownTimer = 0f;
    public float reactiveFlame = 3f;
    public float desactiveFlame = 5f;
    void Start()
    {
        flame.SetActive(false);
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (!infinite)
        {
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
        else 
        {
            flame.SetActive(true);
        }
    }
}
