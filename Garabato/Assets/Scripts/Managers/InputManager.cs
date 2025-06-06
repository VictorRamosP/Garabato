using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    // Teclado
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode attackKey = KeyCode.Mouse0;
    private KeyCode moveLeftKey = KeyCode.A;
    private KeyCode moveRightKey = KeyCode.D;
    private KeyCode rotateMapLeftKey = KeyCode.A;
    private KeyCode rotateMapRightKey = KeyCode.D;
    private KeyCode mapKey = KeyCode.Tab;
    private KeyCode pauseKey = KeyCode.Escape;
    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;
    private KeyCode interactKey = KeyCode.E;

    // Mando
    private KeyCode joystickJumpKey = KeyCode.JoystickButton1;
    private KeyCode joystickAttackKey = KeyCode.JoystickButton5;
    private KeyCode joystickMapKey = KeyCode.JoystickButton0;
    private KeyCode joystickPauseKey = KeyCode.JoystickButton9;
    private KeyCode joystickRotateMapLeftKey = KeyCode.JoystickButton4;
    private KeyCode joystickRotateMapRightKey = KeyCode.JoystickButton5;
    private KeyCode joystickInteractKey = KeyCode.JoystickButton1;

    private Vector3 lastMousePosition;
    public enum InputSource
    {
        None,
        Keyboard,
        Joystick
    }
    public InputSource currentInputSource { get; private set; } = InputSource.None;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadBindings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.mousePosition != lastMousePosition)
        {
            currentInputSource = InputSource.Keyboard;
        }
        lastMousePosition = Input.mousePosition;
    }
    public bool GetJump()
    {
        if (Input.GetKey(jumpKey))
        {
            currentInputSource = InputSource.Keyboard;
            return true;
        }
        else if (Input.GetKey(joystickJumpKey))
        {
            currentInputSource = InputSource.Joystick;
            return true;
        }

        return false;
    }
    public bool GetJumpDown()
    {
        bool inputDetected = false;
        if (Input.GetKeyDown(jumpKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else if (Input.GetKeyDown(joystickJumpKey))
        {
            currentInputSource = InputSource.Joystick;
            inputDetected = true;
        }
        return inputDetected;
    }

    public bool GetJumpUp()
    {
        bool inputDetected = false;
        if (Input.GetKeyUp(jumpKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else if (Input.GetKeyUp(joystickJumpKey))
        {
            currentInputSource = InputSource.Joystick;
            inputDetected = true;
        }
        return inputDetected;
    }

    public bool GetAttack()
    {
        bool inputDetected = false;
        if (Input.GetKeyDown(attackKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else if (Input.GetKeyDown(joystickAttackKey))
        {
            currentInputSource = InputSource.Joystick;
            inputDetected = true;
        }
        return inputDetected;
    }

    public bool GetMoveLeft()
    {
        bool inputDetected = false;

        if (Input.GetKey(moveLeftKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else
        {
            float horizontalAxis = GetHorizontalAxis();
            if (horizontalAxis < -0.1f)
            {
                currentInputSource = InputSource.Joystick;
                inputDetected = true;
            }
        }

        return inputDetected;
    }

    public bool GetMoveRight()
    {
        bool inputDetected = false;

        if (Input.GetKey(moveRightKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else
        {
            float horizontalAxis = GetHorizontalAxis();
            if (horizontalAxis > 0.1f)
            {
                currentInputSource = InputSource.Joystick;
                inputDetected = true;
            }
        }

        return inputDetected;
    }

    public bool GetMap()
    {
        bool inputDetected = false;
        if (Input.GetKeyDown(mapKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else if (Input.GetKeyDown(joystickMapKey))
        {
            currentInputSource = InputSource.Joystick;
            inputDetected = true;
        }
        return inputDetected;
    }

    public bool GetPause()
    {
        bool inputDetected = false;
        if (Input.GetKeyDown(pauseKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else if (Input.GetKeyDown(joystickPauseKey))
        {
            currentInputSource = InputSource.Joystick;
            inputDetected = true;
        }
        return inputDetected;
    }

    public bool GetRotateMapLeft()
    {
        bool inputDetected = false;
        if (Input.GetKeyDown(rotateMapLeftKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else if (Input.GetKeyDown(joystickRotateMapLeftKey))
        {
            currentInputSource = InputSource.Joystick;
            inputDetected = true;
        }
        return inputDetected;
    }

    public bool GetRotateMapRight()
    {
        bool inputDetected = false;
        if (Input.GetKeyDown(rotateMapRightKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else if (Input.GetKeyDown(joystickRotateMapRightKey))
        {
            currentInputSource = InputSource.Joystick;
            inputDetected = true;
        }
        return inputDetected;
    }

    public bool GetUp()
    {
        bool inputDetected = false;

        if (Input.GetKey(upKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else
        {
            float verticalAxis = Input.GetAxisRaw("Vertical");
            if (verticalAxis > 0.5f)
            {
                currentInputSource = InputSource.Joystick;
                inputDetected = true;
            }
        }

        return inputDetected;
    }

    public bool GetDown()
    {
        bool inputDetected = false;

        if (Input.GetKey(downKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else
        {
            float verticalAxis = Input.GetAxisRaw("Vertical");
            if (verticalAxis < 0.5f)
            {
                currentInputSource = InputSource.Joystick;
                inputDetected = true;
            }
        }

        return inputDetected;
    }
    public float GetHorizontalAxis()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool GetInteract()
    {
        bool inputDetected = false;
        if (Input.GetKeyDown(interactKey))
        {
            currentInputSource = InputSource.Keyboard;
            inputDetected = true;
        }
        else if (Input.GetKeyDown(joystickInteractKey))
        {
            currentInputSource = InputSource.Joystick;
            inputDetected = true;
        }
        return inputDetected;
    }

    // Reasignación teclado
    public void Rebind(string action, KeyCode newKey)
    {
        switch (action)
        {
            case "Jump": jumpKey = newKey; break;
            case "Attack": attackKey = newKey; break;
            case "MoveLeft": moveLeftKey = newKey; break;
            case "MoveRight": moveRightKey = newKey; break;
            case "RotateMapLeft": rotateMapLeftKey = newKey; break;
            case "RotateMapRight": rotateMapRightKey = newKey; break;
            case "Map": mapKey = newKey; break;
            case "Pause": pauseKey = newKey; break;
            case "Up": upKey = newKey; break;
        }

        SaveBindings();
    }

    // Reasignación mando
    public void RebindJoystick(string action, KeyCode newKey)
    {
        switch (action)
        {
            case "Jump": joystickJumpKey = newKey; break;
            case "Attack": joystickAttackKey = newKey; break;
            case "RotateMapLeft": joystickRotateMapLeftKey = newKey; break;
            case "RotateMapRight": joystickRotateMapRightKey = newKey; break;
            case "Map": joystickMapKey = newKey; break;
            case "Pause": joystickPauseKey = newKey; break;
        }

        SaveBindings();
    }

    void SaveBindings()
    {
        PlayerPrefs.SetString("Jump", jumpKey.ToString());
        PlayerPrefs.SetString("Attack", attackKey.ToString());
        PlayerPrefs.SetString("MoveLeft", moveLeftKey.ToString());
        PlayerPrefs.SetString("MoveRight", moveRightKey.ToString());
        PlayerPrefs.SetString("RotateMapLeft", rotateMapLeftKey.ToString());
        PlayerPrefs.SetString("RotateMapRight", rotateMapRightKey.ToString());
        PlayerPrefs.SetString("Map", mapKey.ToString());
        PlayerPrefs.SetString("Pause", pauseKey.ToString());
        PlayerPrefs.SetString("Up", upKey.ToString());

        PlayerPrefs.SetString("JoystickJump", joystickJumpKey.ToString());
        PlayerPrefs.SetString("JoystickAttack", joystickAttackKey.ToString());
        PlayerPrefs.SetString("JoystickRotateMapLeft", joystickRotateMapLeftKey.ToString());
        PlayerPrefs.SetString("JoystickRotateMapRight", joystickRotateMapRightKey.ToString());
        PlayerPrefs.SetString("JoystickMap", joystickMapKey.ToString());
        PlayerPrefs.SetString("JoystickPause", joystickPauseKey.ToString());

        PlayerPrefs.Save();
    }

    void LoadBindings()
    {
        if (PlayerPrefs.HasKey("Jump")) jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump"));
        if (PlayerPrefs.HasKey("Attack")) attackKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack"));
        if (PlayerPrefs.HasKey("MoveLeft")) moveLeftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeft"));
        if (PlayerPrefs.HasKey("MoveRight")) moveRightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRight"));
        if (PlayerPrefs.HasKey("RotateMapLeft")) rotateMapLeftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RotateMapLeft"));
        if (PlayerPrefs.HasKey("RotateMapRight")) rotateMapRightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RotateMapRight"));
        if (PlayerPrefs.HasKey("Map")) mapKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Map"));
        if (PlayerPrefs.HasKey("Pause")) pauseKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause"));
        if (PlayerPrefs.HasKey("Up")) upKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up"));

        if (PlayerPrefs.HasKey("JoystickJump")) joystickJumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickJump"));
        if (PlayerPrefs.HasKey("JoystickAttack")) joystickAttackKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickAttack"));
        if (PlayerPrefs.HasKey("JoystickRotateMapLeft")) joystickRotateMapLeftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickRotateMapLeft"));
        if (PlayerPrefs.HasKey("JoystickRotateMapRight")) joystickRotateMapRightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickRotateMapRight"));
        if (PlayerPrefs.HasKey("JoystickMap")) joystickMapKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickMap"));
        if (PlayerPrefs.HasKey("JoystickPause")) joystickPauseKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickPause"));
    }
}