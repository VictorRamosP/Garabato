using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public string LevelToLoad;
    public Button firstSelected;

    private InputManager.InputSource lastInputSource;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        lastInputSource = InputManager.Instance.currentInputSource;

        if (lastInputSource == InputManager.InputSource.Joystick)
        {
            StartCoroutine(SelectFirstButtonNextFrame());
        }
    }

    void Update()
    {
        var currentInput = InputManager.Instance.currentInputSource;

        if (currentInput != lastInputSource)
        {
            lastInputSource = currentInput;

            if (currentInput == InputManager.InputSource.Joystick)
            {
                StartCoroutine(SelectFirstButtonNextFrame());
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    IEnumerator SelectFirstButtonNextFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
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
