using UnityEngine;
using System.Collections;
using Cinemachine;


public class PressurePlate : MonoBehaviour
{
    public Door door; 
    public bool keepOpen = true; // Para indicar si la puerta se queda abierta al pasar una vez o hay que estar encima de la placa para que se abra
    public bool canBeActivatedByPlayer = false;

    private Animator _animatior;
    private bool isPlayerOnPlate = false;
    private bool hasActivated = false;

    public bool haveAnimation = true;


    [Header("Camaras")]
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera camDoorPresuure;
    public float camSwitchDuration = 2f;
    private void Start()
    {
        _animatior = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box") || (canBeActivatedByPlayer && other.CompareTag("Player")))
        {
            if (keepOpen && !hasActivated)
            {
                if (haveAnimation)
                {
                    StartCoroutine(SwitchCameraTemporarily());
                }
                door.Open();
                hasActivated = true;
                _animatior.SetBool("isPressed", true);
            }
            else if (!keepOpen && !isPlayerOnPlate)
            {
                if (haveAnimation)
                {
                    StartCoroutine(SwitchCameraTemporarily());
                }
                door.Open();
                isPlayerOnPlate = true;
                _animatior.SetBool("isPressed", true);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !keepOpen && isPlayerOnPlate)
        {
            //door.Close();
            isPlayerOnPlate = false;
        }
    }
    private IEnumerator SwitchCameraTemporarily()
    {
        if (playerCam == null || camDoorPresuure == null)
        {
            Debug.LogWarning("Camaras no asignadas en PressurePlate.");
            yield break;
        }
        playerCam.Priority = 0;
        camDoorPresuure.Priority = 10;

        

        yield return new WaitForSeconds(camSwitchDuration);

        playerCam.Priority = 10;
        camDoorPresuure.Priority = 0;
    }
}
