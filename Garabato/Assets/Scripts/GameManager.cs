using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool mapAnimationActivated = false;

    [Header("Restricciones")]
    public bool canChangeMap;

    [Header("Condicionales")]
    public bool canPlayerMove;
    public bool canPlayerJump;
    public bool canPlayerShoot;
    public bool canPlayerFall;
    public bool canPlayerGetHurt;

    [Header("Estado")]
    public bool isPaused;
    public bool isCameraReturning;
    public bool isCameraRotating;
    public bool isMapActive;
    public bool isPlayerGrounded;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            isPaused = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (InputManager.Instance.GetPause())
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // Detener el tiempo general
        //Time.timeScale = IsPaused ? 0f : 1f;
    }

    public void setPlayerConstraints(bool set)
    {
        canPlayerMove = set;
        canPlayerJump = set;
        canPlayerShoot = set;
        canPlayerFall = set;
        canPlayerGetHurt = set;
    }
}
