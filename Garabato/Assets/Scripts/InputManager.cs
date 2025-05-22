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

    // Mando
    private KeyCode joystickJumpKey = KeyCode.JoystickButton1;
    private KeyCode joystickAttackKey = KeyCode.JoystickButton5;
    private KeyCode joystickMapKey = KeyCode.JoystickButton0;
    private KeyCode joystickPauseKey = KeyCode.JoystickButton9;
    private KeyCode joystickRotateMapLeftKey = KeyCode.JoystickButton4;
    private KeyCode joystickRotateMapRightKey = KeyCode.JoystickButton5;

    private void Awake()
    {
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

    // Entradas combinadas: teclado o mando
    public bool GetJumpDown() => Input.GetKeyDown(jumpKey) || Input.GetKeyDown(joystickJumpKey);
    public bool GetJumpUp() => Input.GetKeyUp(jumpKey) || Input.GetKeyUp(joystickJumpKey);
    public bool GetAttack() => Input.GetKeyDown(attackKey) || Input.GetKeyDown(joystickAttackKey);
    public bool GetMoveLeft() => Input.GetKey(moveLeftKey) || GetHorizontalAxis() < -0.1f;
    public bool GetMoveRight() => Input.GetKey(moveRightKey) || GetHorizontalAxis() > 0.1f;
    public bool GetMap() => Input.GetKeyDown(mapKey) || Input.GetKeyDown(joystickMapKey);
    public bool GetPause() => Input.GetKeyDown(pauseKey) || Input.GetKeyDown(joystickPauseKey);
    public bool GetRotateMapLeft() => Input.GetKeyDown(rotateMapLeftKey) || Input.GetKeyDown(joystickRotateMapLeftKey);
    public bool GetRotateMapRight() => Input.GetKeyDown(rotateMapRightKey) || Input.GetKeyDown(joystickRotateMapRightKey);
    public bool GetUp() => Input.GetKey(upKey) || Input.GetAxis("Vertical") > 0.5f;

    public float GetHorizontalAxis()
    {
        return Input.GetAxisRaw("Horizontal");
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

        if (PlayerPrefs.HasKey("JoystickJump")) joystickJumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickJump"));
        if (PlayerPrefs.HasKey("JoystickAttack")) joystickAttackKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickAttack"));
        if (PlayerPrefs.HasKey("JoystickRotateMapLeft")) joystickRotateMapLeftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickRotateMapLeft"));
        if (PlayerPrefs.HasKey("JoystickRotateMapRight")) joystickRotateMapRightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickRotateMapRight"));
        if (PlayerPrefs.HasKey("JoystickMap")) joystickMapKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickMap"));
        if (PlayerPrefs.HasKey("JoystickPause")) joystickPauseKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JoystickPause"));
    }
}
