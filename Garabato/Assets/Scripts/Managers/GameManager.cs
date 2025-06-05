using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool mapAnimationActivated;
    public bool playerIntroPlayed = false;
    public bool playerCanMove = false;

    public bool IsPaused { get; private set; } = false;

    public bool AllowCursorLock { get; set; } = true; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPause(bool pause)
    {
        IsPaused = pause;
    }
}
