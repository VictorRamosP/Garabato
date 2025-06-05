using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAnimation : MonoBehaviour
{
    public string nameScene;
   void OnEndAnimation()
    {
        SceneManager.LoadScene(nameScene);
    }
}
