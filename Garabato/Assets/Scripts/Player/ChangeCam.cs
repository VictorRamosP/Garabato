using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    [Header("Referencias")]
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera mapCam;
    private RotateMap rotateMap;
    void Start()
    {
        if (playerCam == null || mapCam == null)
        {
            Debug.LogError("No hay camaras asignadas");
            return;
        }

        GameManager.Instance.isCameraReturning = false;
        GameManager.Instance.isMapActive = false;

        rotateMap = GetComponent<RotateMap>();
    }
    void Update()
    {
        if (rotateMap != null && GameManager.Instance.isCameraRotating)
            return;

        if (InputManager.Instance.GetMap() && (GameManager.Instance.isPlayerGrounded || GameManager.Instance.isMapActive) && GameManager.Instance.mapAnimationActivated && GameManager.Instance.canChangeMap)
        {
            StartCoroutine(SwitchCamera());
        }
    }
    public void Change()
    {
        StartCoroutine(SwitchCamera());
    }
    IEnumerator SwitchCamera()
    {
        GameManager.Instance.isCameraReturning = true;

        GameManager.Instance.setPlayerConstraints(false);

        if (!GameManager.Instance.isMapActive)
        {
            playerCam.Priority = 1;
            mapCam.Priority = 10;

            yield return new WaitForSeconds(1f);

            GameManager.Instance.isMapActive = true;
        }
        else
        {
            playerCam.Priority = 10;
            mapCam.Priority = 0;

            yield return new WaitForSeconds(1f);

            GameManager.Instance.isMapActive = false;

            GameManager.Instance.setPlayerConstraints(true);
        }

        GameManager.Instance.isCameraReturning = false;
    }
}
