using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostitChange : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer Controller;
    public SpriteRenderer Keyboard;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (InputManager.Instance.currentInputSource) {
            case InputManager.InputSource.Keyboard:
                Keyboard.enabled = true;
                Controller.enabled = false;
                break;
            case InputManager.InputSource.Joystick:
                Keyboard.enabled = false;
                Controller.enabled = true;
                break;
            case InputManager.InputSource.None:
                Keyboard.enabled = true;
                Controller.enabled = false;
                break;
        }   
    }
}
