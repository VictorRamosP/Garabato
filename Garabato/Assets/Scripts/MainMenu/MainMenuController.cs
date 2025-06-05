using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public string LevelToLoad;
    public Button firstSelected;

    void Awake()
    {
        StartCoroutine(SelectFirstButtonNextFrame());
    }
    /* 
    void Start()
    {
        StartCoroutine(SelectFirstButtonNextFrame());
    }
    */

    IEnumerator SelectFirstButtonNextFrame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }

    public void PlayGame()
    {
        GameObject.FindAnyObjectByType<CursorManager>().showMouse = false;
        SceneManager.LoadScene("Intro");
    }

    public void LevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    
}
