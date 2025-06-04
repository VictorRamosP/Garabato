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
    private float coolDownTimer;
    private CinemachineFramingTransposer transposer;
    private float targetScreenX;

    void Start()
    {
        coolDownTimer = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        targetScreenX = transposer.m_ScreenX;
    }

    void Update()
    {
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer >= coolDown)
        {
            bool lookingRight = player.transform.localScale.x > 0;
            targetScreenX = lookingRight ? rightOffset : leftOffset;

            transposer.m_ScreenX = Mathf.Lerp(transposer.m_ScreenX, targetScreenX, Time.deltaTime * smoothSpeed);
        }
    }
}
