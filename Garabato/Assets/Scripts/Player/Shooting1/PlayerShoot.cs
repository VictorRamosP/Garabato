using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public KeyCode shootKey = KeyCode.Mouse0;
    private GameObject activeBullet; // Referencia al proyectil activo
    public float coolDawnShoot = 1f;
    private float coolDawnTimer = 0f;
    public float bulletTimeDestroy = 2f; 

    void Update()
    {
        coolDawnTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && coolDawnTimer <= 0f) 
        {
            coolDawnTimer = coolDawnShoot;
            Shoot();
        }
    }

    void Shoot()
    {
        activeBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = activeBullet.GetComponent<Rigidbody2D>();
        

        Destroy(activeBullet, bulletTimeDestroy);
    }
}
