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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene(LevelToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void LoadLevel(string levelName)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene(levelName);
    }
}
