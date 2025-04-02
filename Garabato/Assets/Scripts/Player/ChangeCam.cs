using Cinemachine;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera mapCam;

    [Header("Controles")]
    public KeyCode k_SwitchJump = KeyCode.Tab;

    public static bool isMapActive = false;

    void Start()
    {
        if (playerCam == null || mapCam == null)
        {
            Debug.LogError("No Hay camaras asignadas");
            return;
        }

        playerCam.Priority = 10;
        mapCam.Priority = 0;
        isMapActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(k_SwitchJump))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (!isMapActive)
        {
            playerCam.Priority = 0;
            mapCam.Priority = 10;
            isMapActive = true;
        }
        else
        {
            playerCam.Priority = 10;
            mapCam.Priority = 0;
            isMapActive = false;
        }
    }
}