using Cinemachine;
using UnityEngine;

public class MapIntroChange : MonoBehaviour
{
    //private CinemachineVirtualCamera playerCam;
    private CinemachineVirtualCamera mapCam;
    private CinemachineVirtualCamera gameplayCam;
    public Animator animator;
    public AudioClip drawingClip;
    public bool activateAnimationmap;
    public GameObject Hand;
    public GameObject Book;
    void Start()
    {
        // GameObject playerCamObj = GameObject.FindGameObjectWithTag("PlayerCam");
        GameObject mapCamObj = GameObject.FindGameObjectWithTag("MapCam");
        GameObject gameplayCamObj = GameObject.FindGameObjectWithTag("GameplayCamera");
        // playerCam = playerCamObj.GetComponent<CinemachineVirtualCamera>();
        mapCam = mapCamObj.GetComponent<CinemachineVirtualCamera>();
        gameplayCam = gameplayCamObj.GetComponent<CinemachineVirtualCamera>();
        // Verifica si la animaci�n ya fue mostrada
        if (GameManager.Instance.mapAnimationActivated)
        {
            mapCam.Priority = 0;
            gameplayCam.Priority = 11;
        }
        else
        {
            mapCam.Priority = 10;
            gameplayCam.Priority = 0;
        }
    }
    void Update()
    {

    }
    public void OnMapAnimationEnd()
    {
        gameplayCam.Priority = 11;
        mapCam.Priority = 0;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.mapAnimationActivated = true;
        }
    }

    public void EndTransition()
    {
        if (activateAnimationmap && !GameManager.Instance.mapAnimationActivated)
        {
            animator.SetTrigger("Map");
            gameplayCam.Priority = 0;
            mapCam.Priority = 10;
            Hand.SetActive(true);
            Book.SetActive(true);
            if (drawingClip != null)
            {
                AudioSource.PlayClipAtPoint(drawingClip, Camera.main.transform.position);
            }
        }
        else
        {
            gameplayCam.Priority = 11;
            mapCam.Priority = 0;
            GameManager.Instance.mapAnimationActivated = true;
        }
    }

    public void ExitingScene()
    {
        GameManager.Instance.mapAnimationActivated = false;
    }
}




