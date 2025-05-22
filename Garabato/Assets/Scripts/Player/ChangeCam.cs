using System.Collections;
using Cinemachine;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public bool canChangeMap = true;
    public static bool isReturning;
    public static bool isMapActive = false;

    [Header("Referencias")]
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera mapCam;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    private float gravityScale;
    private Rigidbody2D rb;
    private CollisionDetection collisionDetection;
    private PlayerMove playerMove;
    private PlayerJumper playerJumper;
    private PlayerShoot playerShoot;

    void Start()
    {
        if (playerCam == null || mapCam == null)
        {
            Debug.LogError("No hay cámaras asignadas");
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetection>();
        playerMove = GetComponent<PlayerMove>();
        playerJumper = GetComponent<PlayerJumper>();
        playerShoot = GetComponent<PlayerShoot>();

        gravityScale = rb.gravityScale;
        isReturning = false;
        isMapActive = false;
    }

    void Update()
    {
        RotateMap rotateMap = FindObjectOfType<RotateMap>();
        if (rotateMap != null && rotateMap.IsRotating)
            return;

        if (InputManager.Instance.GetMap() && (collisionDetection.IsGrounded || isMapActive) &&
            GameManager.Instance.mapAnimationActivated &&
            canChangeMap)
        {
            StartCoroutine(SwitchCamera());
        }
    }

    IEnumerator SwitchCamera()
    {
        isReturning = true;

        playerMove.canMove = false;
        playerJumper.canJump = false;
        playerShoot.canShoot = false;

        rb.velocity = Vector2.zero;

        if (!isMapActive)
        {
            playerCam.Priority = 1;
            mapCam.Priority = 10;

            yield return new WaitForSeconds(1f);

            isMapActive = true;
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero; 
        }
        else
        {
            playerCam.Priority = 10;
            mapCam.Priority = 0;

            yield return new WaitForSeconds(1f);

            isMapActive = false;
            rb.gravityScale = gravityScale;

            playerMove.canMove = true;
            playerJumper.canJump = true;
            playerShoot.canShoot = true;
        }

        isReturning = false;
    }
}
