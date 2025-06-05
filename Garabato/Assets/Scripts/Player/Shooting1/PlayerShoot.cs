using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private GameObject activeBullet;

    public float coolDownShoot = 1f;
    private float coolDownTimer = 0f;
    private bool wasUpShoot = false;

    public GameObject shooting;
    public PlayerMove _playermove;

    public AudioClip shootSound;
    private AudioSource _audioSource;

    public float weaponVisible = 0.2f;

    public Sprite sideShootSprite;
    public Sprite upShootSprite;
    public Sprite diagonalShootSprite;

    public SpriteRenderer weaponRenderer;

    private CollisionDetection _collisionDetection;
    public bool canShoot;

    public ParticleSystem shootParticles;

    void Start()
    {
        canShoot = true;
        //shooting = GameObject.FindGameObjectWithTag("Weapon");

        if (shooting == null)
        {
            Debug.LogError("No se encontró ningún objeto con tag 'Weapon' en la escena.");
            return;
        }

        //weaponRenderer = shooting.GetComponent<SpriteRenderer>();

        if (weaponRenderer == null)
        {
            Debug.LogError("El objeto 'Weapon' no tiene SpriteRenderer.");
            return;
        }

        _collisionDetection = GetComponent<CollisionDetection>();

        if (weaponRenderer != null && sideShootSprite != null)
        {
            weaponRenderer.sprite = sideShootSprite;
        }
    }

    void Update()
    {
        if (!canShoot) return;
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

        // Destroy(activeBullet, bulletTimeDestroy);
        if (_audioSource && shootSound)
        {
            _audioSource.PlayOneShot(shootSound);
        }
        if (shootParticles != null)
        {
            shootParticles.Play();
        }
    }

    void RotateShoot()
    {
        if (_collisionDetection == null || !_collisionDetection.IsGrounded || !GameManager.Instance.mapAnimationActivated)
            return;

        bool Shot = false;

         if (InputManager.Instance.GetAttack() && InputManager.Instance.GetUp() &&
            (InputManager.Instance.GetMoveLeft() || InputManager.Instance.GetMoveRight()))
            {
            wasUpShoot = false;

            if (_playermove.mirandoDerecha)
            {
                shooting.transform.rotation = Quaternion.Euler(0, 0, -18);
                firePoint.localRotation = Quaternion.Euler(0, 0, -45);
                firePoint.localPosition = new Vector3(0.49f, 0.7f, 0f);
            }
            else
            {
                shooting.transform.rotation = Quaternion.Euler(0, 0, 9);
                firePoint.localRotation = Quaternion.Euler(0, 0, -45);
                firePoint.localPosition = new Vector3(0.49f, 0.7f, 0f);
            }

            if (weaponRenderer && diagonalShootSprite)
                weaponRenderer.sprite = diagonalShootSprite;

            Shot = true;
        }
        else if (InputManager.Instance.GetUp() && InputManager.Instance.GetAttack())
        {
            wasUpShoot = true;

            shooting.transform.rotation = Quaternion.Euler(0, 0, 0);
            firePoint.localRotation = Quaternion.Euler(0, 0, 0);
            firePoint.localPosition = new Vector3(0f, 1f, 0f);

            if (weaponRenderer && upShootSprite)
                weaponRenderer.sprite = upShootSprite;

            Shot = true;
        }
        else if (InputManager.Instance.GetAttack())
        {
            wasUpShoot = false;

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

        if (wasUpShoot && weaponRenderer && sideShootSprite)
        {
            weaponRenderer.sprite = sideShootSprite;
            wasUpShoot = false; 
        }

        bool isMoving = InputManager.Instance.GetMoveLeft() || InputManager.Instance.GetMoveRight();

        if (isMoving)
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
