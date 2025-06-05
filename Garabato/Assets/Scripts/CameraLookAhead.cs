using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAhead : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private GameObject player;
    public float leftOffset;
    public float rightOffset;
    public float smoothSpeed = 2f;
    public float coolDown;
    public float upwardOffset = 0.2f; 
    private float coolDownTimer;
    private CinemachineFramingTransposer transposer;
    private float targetScreenX;
    private float defaultScreenY;
    private float targetScreenY;

    void Start()
    {
        coolDownTimer = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        targetScreenX = transposer.m_ScreenX;
        defaultScreenY = transposer.m_ScreenY;
        targetScreenY = defaultScreenY;
    }

    void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (coolDownTimer >= coolDown)
        {
            bool lookingRight = player.transform.localScale.x > 0;
            targetScreenX = lookingRight ? rightOffset : leftOffset;
        }

        if (InputManager.Instance.GetUp() &&
            !InputManager.Instance.GetMoveLeft() &&
            !InputManager.Instance.GetMoveRight())
        {
            targetScreenY = defaultScreenY + upwardOffset;
        }
        else
        {
            targetScreenY = defaultScreenY;
        }

        transposer.m_ScreenX = Mathf.Lerp(transposer.m_ScreenX, targetScreenX, Time.deltaTime * smoothSpeed);
        transposer.m_ScreenY = Mathf.Lerp(transposer.m_ScreenY, targetScreenY, Time.deltaTime * smoothSpeed);
    }

}
