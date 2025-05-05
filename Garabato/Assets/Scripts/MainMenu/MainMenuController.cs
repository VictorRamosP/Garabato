using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay"); 
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