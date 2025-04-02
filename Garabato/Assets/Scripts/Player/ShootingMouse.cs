using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingMouse : MonoBehaviour
{
    public Transform mira;
    public Transform arm;
    public SpriteRenderer gunSR;
    public Transform bulletPos;
    public int speedball;
    public float cooldawnShoot = 0.5f;
    Vector3 targetRotation;
    public GameObject ball;
    Vector3 finaltarget;
    private PlayerMove playerMove;
    private bool canShoot = true;

    [Header("Controles")]
    public KeyCode k_mouse0 = KeyCode.Mouse0;


    private void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        /* if (GamePauseManager.isPaused)
         {
             return;
         }*/
        if (!ChangeCam.isMapActive)
        {
            mira.gameObject.SetActive(true);

            mira.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;

            if (!playerMove.mirandoDerecha)
            {
                angle += 180;
            }

            arm.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (angle > 90 || angle < -90)
                gunSR.flipY = true;
            else
                gunSR.flipY = false;

            if (Input.GetKeyDown(k_mouse0) && canShoot)
            {
                StartCoroutine(ShootWithCooldown());
            }
        }
        else
        {
            mira.gameObject.SetActive(false);
        }
        
    }

    private IEnumerator ShootWithCooldown()
    {
        Shoot();
        canShoot = false;
        yield return new WaitForSeconds(cooldawnShoot);
        canShoot = true;
    }

    void Shoot()
    {
        //AudioManager.Instance.ReproducirSonido(shootClip);
        var Ball = Instantiate(ball, bulletPos.position, Quaternion.identity);
        targetRotation.z = 0;
        finaltarget = (targetRotation - transform.position).normalized;

        Ball.GetComponent<Rigidbody2D>().AddForce(finaltarget * speedball, ForceMode2D.Impulse);
        Destroy(Ball, 2f);
    }
}
