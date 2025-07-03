using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Controller;
    public GameObject Keyboard;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (InputManager.Instance.currentInputSource) {
            case InputManager.InputSource.Keyboard:
                Keyboard.SetActive(true);
                Controller.SetActive(false);
                break;
            case InputManager.InputSource.Joystick:
                Keyboard.SetActive(false);
                Controller.SetActive(true);
                break;
            case InputManager.InputSource.None:
                Keyboard.SetActive(true);
                Controller.SetActive(false);
                break;
        }   
    }
}
