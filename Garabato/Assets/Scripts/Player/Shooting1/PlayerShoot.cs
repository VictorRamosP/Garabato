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

    public AudioClip shootSound;
    private AudioSource _audioSource;

    public float weaponVisible = 0.2f;

    public Sprite sideShootSprite;
    public Sprite upShootSprite;

    private SpriteRenderer weaponRenderer;

    private CollisionDetection _collisionDetection;

    void Start()
    {
        shooting = GameObject.FindGameObjectWithTag("Weapon");
        _audioSource = GetComponent<AudioSource>();
        weaponRenderer = shooting.GetComponent<SpriteRenderer>();
        _collisionDetection = GetComponent<CollisionDetection>();

        if (weaponRenderer != null && sideShootSprite != null)
        {
            weaponRenderer.sprite = sideShootSprite;
        }
    }

    void Update()
    {
        coolDownTimer -= Time.deltaTime;

        if (!ChangeCam.isMapActive && coolDownTimer <= 0)
        {
            RotateShoot();
        }
    }

    void Shoot()
    {
        activeBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = activeBullet.GetComponent<Rigidbody2D>();

        Destroy(activeBullet, bulletTimeDestroy);

        if (_audioSource && shootSound)
        {
            _audioSource.PlayOneShot(shootSound);
        }
    }

    void RotateShoot()
    {
        if (_collisionDetection == null || !_collisionDetection.IsGrounded)
            return;

        bool Shot = false;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.Mouse0))
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 0);
            firePoint.localRotation = Quaternion.Euler(0, 0, 0);
            firePoint.localPosition = new Vector3(0f, 1f, 0f);

            if (weaponRenderer && upShootSprite)
                weaponRenderer.sprite = upShootSprite;

            Shot = true;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_playermove.mirandoDerecha)
            {
                shooting.transform.rotation = Quaternion.Euler(0, 0, 0);
                firePoint.localRotation = Quaternion.Euler(0, 0, -90);
                firePoint.localPosition = new Vector3(1f, 0f, 0f);
            }
            else
            {
                shooting.transform.rotation = Quaternion.Euler(0, 0, 0);
                firePoint.localRotation = Quaternion.Euler(0, 0, -90);
                firePoint.localPosition = new Vector3(1f, 0f, 0f);
            }

            if (weaponRenderer && sideShootSprite)
                weaponRenderer.sprite = sideShootSprite;

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
