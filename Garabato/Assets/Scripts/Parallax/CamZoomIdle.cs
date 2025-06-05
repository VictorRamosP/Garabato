using UnityEngine;
using Cinemachine;

public class CameraZoomOnIdle : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform player;

    public float idleTimeToZoom = 2f;
    public float zoomedSize = 4f;
    public float normalSize = 5f;
    public float zoomSpeed = 1f;

    private float idleTimer = 0f;
    private Vector2 lastPosition;

    void Start()
    {
        lastPosition = player.position;
    }

    void Update()
    {
        if (player == null || virtualCamera == null) return;

        Vector2 currentPosition = player.position;

        if (Vector2.Distance(currentPosition, lastPosition) < 0.01f)
        {
            idleTimer += Time.deltaTime;
        }
        else
        {
            idleTimer = 0f;
        }

        float targetSize = (idleTimer >= idleTimeToZoom) ? zoomedSize : normalSize;

        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(
            virtualCamera.m_Lens.OrthographicSize,
            targetSize,
            Time.deltaTime * zoomSpeed
        );

        lastPosition = currentPosition;
    }
}