using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public bool canChangeMap = true;
    private CollisionDetection _collisionDetection;
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera mapCam;
    private float gravityScale;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    /*
    [Header("Controles")]
    public KeyCode k_SwitchJump = KeyCode.Tab;
    */
    public static bool isMapActive = false;

    void Start()
    {
        if (playerCam == null || mapCam == null)
        {
            Debug.LogError("No Hay camaras asignadas");
            return;
        }

        /*playerCam.Priority = 0;
        mapCam.Priority = 10;*/
        isMapActive = false;
        gravityScale = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        _collisionDetection = gameObject.GetComponent<CollisionDetection>();
    }

    void Update()
    {
        // Evita que cambie la camara si el mapa esta rotando
        RotateMap rotateMap = FindObjectOfType<RotateMap>();
        if (rotateMap != null && rotateMap.IsRotating)
            return;

        if (InputManager.Instance.GetMap() && (_collisionDetection.IsGrounded || isMapActive)
            && GameManager.Instance.mapAnimationActivated && canChangeMap)
        {
            StartCoroutine(SwitchCamera());
        }
    }

    IEnumerator SwitchCamera()
    {
        FindAnyObjectByType<PlayerMove>().canMove = false;
        FindAnyObjectByType<PlayerJumper>().canJump = false;
        FindAnyObjectByType<PlayerShoot>().canShoot = false;
        if (!isMapActive)
        {
            playerCam.Priority = 1;
            mapCam.Priority = 10;
            yield return new WaitForSeconds(1f);
            isMapActive = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {

            playerCam.Priority = 10;
            mapCam.Priority = 0;
            yield return new WaitForSeconds(1f);
            isMapActive = false;
            FindAnyObjectByType<PlayerMove>().canMove = true;
            FindAnyObjectByType<PlayerJumper>().canJump = true;
            FindAnyObjectByType<PlayerShoot>().canShoot = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

        }

    }
}
