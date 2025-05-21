using Cinemachine;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
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
         for (int i = 0; i < 20; i++)
        {
        if (Input.GetKeyDown("joystick button " + i))
        {
            Debug.Log("Joystick button " + i + " pressed");
        }
        }
        // Evita que cambie la camara si el mapa esta rotando
        RotateMap rotateMap = FindObjectOfType<RotateMap>();
        if (rotateMap != null && rotateMap.IsRotating)
            return;

        if (InputManager.Instance.GetMap() && (_collisionDetection.IsGrounded || isMapActive)
            && GameManager.Instance.mapAnimationActivated)
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (!isMapActive)
        {
            playerCam.Priority = 0;
            mapCam.Priority = 10;
            isMapActive = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            playerCam.Priority = 10;
            mapCam.Priority = 0;
            isMapActive = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
    }
}
