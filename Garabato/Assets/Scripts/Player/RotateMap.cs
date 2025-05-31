using System;
using System.Collections;
using System.Data;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    private GameObject map;
    private float cooldownTimer = 0f;
    public event Action OnMapRotated;
    public AudioClip rotateSound;
    private GameObject player;
    private AudioSource _audioSource;

    public float cooldownRotate = 0.5f;
    public float rotationDuration = 0.5f;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        map = GameObject.FindGameObjectWithTag("Map");

        _audioSource = GetComponent<AudioSource>();

        if (map == null)
        {
            Debug.LogError("No se ha encontrado un Mapa. Pon el tag 'Map' al mapa.");
        }
    }
    void Update()
    {
        if (GameManager.Instance.isMapActive)
        {
            if (player == null) player = GameObject.FindGameObjectWithTag("Player");
            if (map == null) map = GameObject.FindGameObjectWithTag("Map");
            player.transform.SetParent(map.transform);
            player.transform.rotation = Quaternion.identity;
            Rotate();
        }
    }
    void Rotate()
    {
        if (GameManager.Instance.isCameraReturning) return;
        cooldownTimer -= Time.deltaTime;

        if ((InputManager.Instance.GetRotateMapRight() || InputManager.Instance.GetRotateMapLeft()) && cooldownTimer <= 0f && !GameManager.Instance.isCameraRotating)
        {
            float angle = (InputManager.Instance.GetRotateMapLeft()) ? 90f : -90f;
            StartCoroutine(SmoothRotate(angle));
            cooldownTimer = cooldownRotate;
        }
    }

    IEnumerator SmoothRotate(float angle)
    {

        GameManager.Instance.isCameraRotating = true;

        _audioSource?.PlayOneShot(rotateSound);

        Quaternion startRotation = map.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, angle);

        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / rotationDuration);
            map.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        map.transform.rotation = endRotation;

        GameManager.Instance.isCameraRotating = false;
        OnMapRotated?.Invoke();
    }
}
