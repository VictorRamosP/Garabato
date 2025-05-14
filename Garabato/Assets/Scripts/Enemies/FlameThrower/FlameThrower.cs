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
    public bool isActive; 
    void Start()
    {
        flame.SetActive(false);
        isActive = true;
    }

    void Update()
    {
        if (!isActive) {
            flame.SetActive(false);
            return;
        }
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
