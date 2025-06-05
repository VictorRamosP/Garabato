using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public string LevelToLoad;
    public Button firstSelected;

    void Start()
    {
        GameManager.Instance.AllowCursorLock = false; 
        CursorManager.UnlockCursor();
        StartCoroutine(SelectFirstButtonNextFrame());
    }

    void Awake()
    {
        StartCoroutine(SelectFirstButtonNextFrame());
    }

    IEnumerator SelectFirstButtonNextFrame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }

    public void PlayGame()
    {
        GameManager.Instance.AllowCursorLock = true;
        GameObject.FindAnyObjectByType<CursorManager>().showMouse = false;
        CursorManager.LockCursor();
        SceneManager.LoadScene("Intro");
    }

    public void LevelSelector()
    {
        GameManager.Instance.AllowCursorLock = true;
        GameObject.FindAnyObjectByType<CursorManager>().showMouse = false;
        CursorManager.LockCursor();
        SceneManager.LoadScene("LevelSelector");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void LoadLevel(string levelName)
    {
        GameManager.Instance.AllowCursorLock = true;
        GameObject.FindAnyObjectByType<CursorManager>().showMouse = false;
        CursorManager.LockCursor();
        SceneManager.LoadScene(levelName);
    }
}
