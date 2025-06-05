using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Update()
    {
        if (InputManager.Instance.GetPause())
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        bool newPauseState = !GameManager.Instance.IsPaused;
        GameManager.Instance.SetPause(newPauseState);

        pauseMenuUI.SetActive(newPauseState);

        if (newPauseState)
        {
            CursorManager.UnlockCursor();
            Time.timeScale = 0;
        }
        else
        {
            CursorManager.LockCursor();
            Time.timeScale = 1;
        }
    }

    public void ResumeGame()
    {
        GameManager.Instance.SetPause(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        CursorManager.LockCursor();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.SetPause(false);
        GameManager.Instance.AllowCursorLock = false; 
        pauseMenuUI.SetActive(false);

        CursorManager cm = FindObjectOfType<CursorManager>();
        if (cm != null)
        {
            cm.showMouse = true;
            cm.ApplyCursorState();
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
