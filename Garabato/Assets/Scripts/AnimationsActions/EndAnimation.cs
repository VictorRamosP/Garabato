using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAnimation : MonoBehaviour
{
    public string nameScene;
    void Update()
    {
        if (InputManager.Instance.GetPause())
        {
            SceneManager.LoadScene(nameScene);
        }
    }
    void OnEndAnimation()
    {
        SceneManager.LoadScene(nameScene);
    }
}
