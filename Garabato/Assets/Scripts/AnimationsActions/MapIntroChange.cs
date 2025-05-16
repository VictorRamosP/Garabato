using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIntroChange : MonoBehaviour
{
    private CinemachineVirtualCamera playerCam;
    private CinemachineVirtualCamera mapCam;
    public AudioClip drawingClip;
    
    void Start()
    {
        GameObject playerCamObj = GameObject.FindGameObjectWithTag("PlayerCam");
        GameObject mapCamObj = GameObject.FindGameObjectWithTag("MapCam");
        playerCam = playerCamObj.GetComponent<CinemachineVirtualCamera>();
        mapCam = mapCamObj.GetComponent<CinemachineVirtualCamera>();

        // Verifica si la animación ya fue mostrada
        if (GameManager.Instance != null && GameManager.Instance.mapAnimationActivated)
        {
            playerCam.Priority = 10;
            mapCam.Priority = 0;
          
            gameObject.SetActive(false);
        }
        else
        {
            playerCam.Priority = 0;
            mapCam.Priority = 10;

            if (drawingClip != null)
            {
                AudioSource.PlayClipAtPoint(drawingClip, Camera.main.transform.position);
            }
            
        }
    }
    public void OnMapAnimationEnd()
    {
        playerCam.Priority = 10;
        mapCam.Priority = 0;
        gameObject.SetActive(false);

        // Marcar que la animación ya se mostró
        if (GameManager.Instance != null)
        {
            GameManager.Instance.mapAnimationActivated = true;
        }
    }

    
}




