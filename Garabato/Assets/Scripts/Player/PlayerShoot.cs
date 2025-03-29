using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public KeyCode shootKey = KeyCode.Mouse0;
    private GameObject activeBullet; // Referencia al proyectil activo
    public float timeDestroy = 2f; //tiempo que tarda en destruirse una bala. Hasta que no se destruya no puedes volver a disparar

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && activeBullet == null) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        activeBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = activeBullet.GetComponent<Rigidbody2D>();
        

        Destroy(activeBullet, timeDestroy);
    }
}
