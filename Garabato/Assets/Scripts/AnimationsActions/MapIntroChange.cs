using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIntroChange : MonoBehaviour
{
    private CinemachineVirtualCamera playerCam;
    private CinemachineVirtualCamera mapCam;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        GameObject playerCamObj = GameObject.FindGameObjectWithTag("PlayerCam");
        GameObject mapCamObj = GameObject.FindGameObjectWithTag("MapCam");
        playerCam = playerCamObj.GetComponent<CinemachineVirtualCamera>();
        mapCam = mapCamObj.GetComponent<CinemachineVirtualCamera>();

        playerCam.Priority = 0;
        mapCam.Priority = 10;

        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1f; 
        spriteRenderer.color = color;
    }

    
    public void OnMapAnimationEnd()
    {
        playerCam.Priority = 10;
        mapCam.Priority = 0;
        gameObject.SetActive(false);
    }
}
