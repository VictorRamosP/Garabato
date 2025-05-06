using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    //public KeyCode shootKey = KeyCode.Mouse0;
    private GameObject activeBullet; // Referencia al proyectil activo
    public float coolDownShoot = 1f;
    private float coolDownTimer = 0f;
    public float bulletTimeDestroy = 2f; 
    private GameObject shooting;
    void Start()
    {
        shooting = GameObject.FindGameObjectWithTag("Weapon");
    }
    void Update()
    {
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer <= 0) {
            RotateShoot();
        } 
    }

    void Shoot()
    {
        activeBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = activeBullet.GetComponent<Rigidbody2D>();
        

        Destroy(activeBullet, bulletTimeDestroy);
    }

    void RotateShoot()
    {
        bool Shot = false;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, -45);
            Shot = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 45); 
            Shot = true;
        }else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 135); 
            Shot = true;
        }else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, -135); 
            Shot = true;
        }else if (Input.GetKey(KeyCode.RightArrow)) {
            shooting.transform.rotation = Quaternion.Euler(0, 0, -90);
            Shot = true;
        }else if (Input.GetKey(KeyCode.LeftArrow)) {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 90);
            Shot = true;
        }else if (Input.GetKey(KeyCode.UpArrow))
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 0);
            Shot = true;
        }else if (Input.GetKey(KeyCode.DownArrow)) {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 180);
            Shot = true;
        }

        if (Shot) {
            coolDownTimer = coolDownShoot;
            Shoot();
        }
        
    }
}
