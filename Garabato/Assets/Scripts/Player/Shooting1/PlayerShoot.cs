using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private GameObject activeBullet;

    public float coolDownShoot = 1f;
    private float coolDownTimer = 0f;
    public float bulletTimeDestroy = 2f;

    private GameObject shooting;
    public PlayerMove _playermove;

    public float weaponVisible = 0.2f; // Tiempo que es visible el arma cuando dispara estando en Idle

    void Start()
    {
        shooting = GameObject.FindGameObjectWithTag("Weapon");
    }

    void Update()
    {
        coolDownTimer -= Time.deltaTime;

        if (!ChangeCam.isMapActive)
        {
            if (coolDownTimer <= 0)
            {
                RotateShoot();
            }
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

        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && _playermove.mirandoDerecha)
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, -45);
            Shot = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && !_playermove.mirandoDerecha)
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 45);
            Shot = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && !_playermove.mirandoDerecha)
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 135);
            Shot = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow) && _playermove.mirandoDerecha)
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, -135);
            Shot = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && _playermove.mirandoDerecha)
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, -90);
            Shot = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !_playermove.mirandoDerecha)
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 90);
            Shot = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 0);
            Shot = true;
        }

        if (Shot)
        {
            shooting.SetActive(true);
            _playermove.shootingActive = true;

            coolDownTimer = coolDownShoot;
            Shoot();

            StartCoroutine(DisableWeaponAfterShot());
        }
    }

    IEnumerator DisableWeaponAfterShot()
    {
        yield return new WaitForSeconds(weaponVisible);

        float moveInput = Mathf.Abs(Input.GetAxis("Horizontal"));
        if (moveInput > 0.01f)
        {
            
            _playermove.shootingActive = false;
        }
        else
        {
            shooting.SetActive(false);
            _playermove.shootingActive = false;
        }
    }
}
