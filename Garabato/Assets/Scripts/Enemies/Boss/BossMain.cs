using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BossMain : MonoBehaviour
{
    private StateMachine stateMachine;
    private float Health;

    [HideInInspector]
    public RotateMap Map;

    [Header("Variables")]
    public float MoveSpeed = 5f;
    public float ChangePositionCooldown = 0f;
    public float VidaRestanteParaFase2 = 0f;
    public float VidaRestanteParaFase3 = 0f;
    public float PorcentajeDeSpawnFase1 = 0f;
    public float PorcentajeDeSpawnFase2 = 0f;
    public float PorcentajeDeSpawnFase3 = 0f;

    [Header("O Spawnea un enemigo o lanza pinchos")]
    public float PorcentajeDeLanzarPinchosFase1 = 0f;
    public float PorcentajeDeLanzarPinchosFase2 = 0f;
    public float PorcentajeDeLanzarPinchosFase3 = 0f;

    [Header("Variables")]
    public float TimeForSpikesToFall = 0f;
    public float SpikeSpeed = 0f;

    [Header("Scripts de funciones")]
    public Transform Center;
    public EnemyLife HealthScript;
    public GameObject Hand;
    public GameObject SpawnArea;
    public SpawnpointHandler Spawnpoint;
    public SpikeareaHandler UpSpikeArea;
    public SpikeareaHandler DownSpikeArea;
    public SpikeareaHandler LeftSpikeArea;
    public SpikeareaHandler RightSpikeArea;
    public GameObject Mapa;

    [Header("Spawnable Enemies")]
    public GameObject Bat;
    public GameObject Goomba;
    public GameObject Spider;
    public GameObject Spike1;
    public GameObject Spike2;
    public GameObject Spike3;
    public GameObject Spike4;

    void Start()
    {
        Health = HealthScript.health;
        Map = FindAnyObjectByType<RotateMap>();
        Mapa = GameObject.FindWithTag("Map");

        Map.OnMapRotated += OnMapRotated;

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new CombatState(this, stateMachine, 1));
    }

    // Update is called once per frame
    void Update()
    {
        Health = HealthScript.health;

        if (Health <= VidaRestanteParaFase2 && Health > VidaRestanteParaFase3)
        {
            //stateMachine.ChangeState();
        }
        else if (Health <= VidaRestanteParaFase3)
        {
            //stateMachine.ChangeState();
        }

        stateMachine.OnUpdate();
    }
    
    void OnMapRotated()
    {
        Hand.transform.rotation = Quaternion.identity;
    }
}
