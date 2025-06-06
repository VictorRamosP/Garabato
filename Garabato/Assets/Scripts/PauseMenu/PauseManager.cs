using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject firstSelectedButton;

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
            Time.timeScale = 0;

            // Seleccionar el primer bot�n para navegaci�n con mando
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
        else
        {
            Time.timeScale = 1;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ResumeGame()
    {
        GameManager.Instance.SetPause(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(null);
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