using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera mapCam;
    public KeyCode switchKey = KeyCode.Tab;

    private CinemachineVirtualCamera activeCamera;

    void Start()
    {
        if (playerCam == null || mapCam == null)
        {
            Debug.LogError("No Hay camaras asignadas");
            return;
        }


        activeCamera = playerCam;
        playerCam.Priority = 10;
        mapCam.Priority = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (activeCamera == playerCam)
        {
            playerCam.Priority = 0;
            mapCam.Priority = 10;
            activeCamera = mapCam;
        }
        else
        {
            playerCam.Priority = 10;
            mapCam.Priority = 0;
            activeCamera = playerCam;
        }
    }
}
