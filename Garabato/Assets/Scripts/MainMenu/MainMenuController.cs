using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string LevelToLoad;
    public void PlayGame()
    {
        GameManager.Instance.mapAnimationActivated = false;
        SceneManager.LoadScene("LvL-1"); 
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            LoadLevel(LevelToLoad);
        }
    }
}