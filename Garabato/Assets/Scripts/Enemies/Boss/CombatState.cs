using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static SpikeareaHandler;

public class CombatState : IState
{
    BossMain Boss;
    StateMachine stateMachine;
    byte fase;
    float SpawnChance;
    float SpikeChance;
    Vector2 targetPosition;
    float cooldown;
    bool hasArrived = false;
    bool isSpikeAttacking = false;

    public CombatState(BossMain _boss, StateMachine _stateMachine, byte _fase)
    {
        this.Boss = _boss;
        this.stateMachine = _stateMachine;
        this.fase = _fase;
    }
    public void OnEnter()
    {
        isSpikeAttacking = false;
        targetPosition = GetNewTargetPosition();
        Boss.Map.OnMapRotated += OnMapRotated;
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        SetSpawnChance();
        if (isSpikeAttacking) return;

        if (!hasArrived)
        {
            Boss.Hand.transform.position = Vector2.MoveTowards(Boss.Hand.transform.position, targetPosition, Boss.MoveSpeed * Time.deltaTime);
            if (Vector2.Distance(Boss.Hand.transform.position, targetPosition) < 0.05f)
            {
                hasArrived = true;
                cooldown = 0f;
            }
        }
        else
        {
            cooldown += Time.deltaTime;

            if (cooldown >= Boss.ChangePositionCooldown)
            {
                if (d100() <= SpawnChance)
                {
                    if (d100() <= SpikeChance)
                    {
                        SpikeAttack();
                    }
                    else
                    {
                        SpawnEnemy();
                    }
                }

                targetPosition = GetNewTargetPosition();
                hasArrived = false;
            }
        }
    }
    int d100()
    {
        return Random.Range(0, 101);
    }

    void OnMapRotated()
    {
        targetPosition = GetNewTargetPosition();
    }
    public Vector2 GetNewTargetPosition()
    {
        BoxCollider2D box = Boss.SpawnArea.GetComponent<BoxCollider2D>();

        Vector2 localPoint = new Vector2(
            Random.Range(-0.5f, 0.5f) * box.size.x,
            Random.Range(-0.5f, 0.5f) * box.size.y
        );

        localPoint += box.offset;

        Vector2 worldPoint = Boss.SpawnArea.transform.TransformPoint(localPoint);

        return worldPoint;
    }

    void SpawnEnemy()
    {
        if (!Boss.Spawnpoint.IsPlayerInSpawnpoint)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    Object.Instantiate(Boss.Bat, Boss.Spawnpoint.Spawnpoint, Quaternion.identity, Boss.Mapa.transform);
                    break;
                case 2:
                    Object.Instantiate(Boss.Goomba, Boss.Spawnpoint.Spawnpoint, Quaternion.identity, Boss.Mapa.transform);
                    break;
            }
        }
    }
    SpikeareaHandler SelectSpikeArea()
    {
        bool isValid = false;
        SpikeareaHandler selectedSpikeArea = null;
        while (!isValid)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    selectedSpikeArea = Boss.UpSpikeArea;
                    break;
                case 2:
                    selectedSpikeArea = Boss.DownSpikeArea;
                    break;
                case 3:
                    selectedSpikeArea = Boss.RightSpikeArea;
                    break;
                case 4:
                    selectedSpikeArea = Boss.LeftSpikeArea;
                    break;
            }

            if (!selectedSpikeArea.IsPlayerInSpawnpoint)
            {
                isValid = true;
            }
        }
        return selectedSpikeArea;
    }
    void SpikeAttack()
    {
        Boss.StartCoroutine(SpikeAttackRoutine());
    }

    IEnumerator SpikeAttackRoutine()
    {
        isSpikeAttacking = true;

        SpikeareaHandler selectedSpikeArea = SelectSpikeArea();
        List<GameObject> spawnedSpikes = new List<GameObject>();
        Quaternion rotation = Quaternion.identity;
         
        foreach (GameObject spawner in selectedSpikeArea.Spawners)
        {

            Vector2 spawnPos = spawner.transform.position;
            switch (selectedSpikeArea.AreaDirection)
            {
                case Direction.up:
                    rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case Direction.down:
                    rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Direction.right:
                    rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case Direction.left:
                    rotation = Quaternion.Euler(0, 0, -90);
                    break;
            }

            while (Vector2.Distance(Boss.Hand.transform.position, spawner.transform.position) > 0.01f)
            {
                Boss.Hand.transform.position = Vector2.MoveTowards(
                    Boss.Hand.transform.position,
                    spawner.transform.position,
                    Boss.MoveSpeed * Time.deltaTime
                );
                yield return null;
            }

            GameObject spike = Object.Instantiate(ChooseSpike(), spawner.transform.position, rotation, Boss.Mapa.transform);
            spawnedSpikes.Add(spike);

            yield return new WaitForSeconds(0.1f); // Spawn delay
        }

        // Esperar cooldown final antes de hacerlos caer
        targetPosition = GetNewTargetPosition();
        hasArrived = false;
        isSpikeAttacking = false;

        yield return new WaitForSeconds(Boss.TimeForSpikesToFall);

        foreach (GameObject spike in spawnedSpikes)
        {
            // Lanzar pinchos
            spike.GetComponent<PinchoLanzable>().speed = Boss.SpikeSpeed;
            spike.GetComponent<PinchoLanzable>().Launch();
        }
    }

    GameObject ChooseSpike()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                return Boss.Spike1;
            case 2:
                return Boss.Spike2;
            case 3:
                return Boss.Spike3;
            case 4:
                return Boss.Spike4;
            default:
                return Boss.Spike1;
        }
    }
    void SetSpawnChance()
    {
        switch (fase)
        {
            case 1:
                SpawnChance = Boss.PorcentajeDeSpawnFase1;
                SpikeChance = Boss.PorcentajeDeLanzarPinchosFase1;
                break;
            case 2:
                SpawnChance = Boss.PorcentajeDeSpawnFase2;
                SpikeChance = Boss.PorcentajeDeLanzarPinchosFase2;
                break;
            case 3:
                SpawnChance = Boss.PorcentajeDeSpawnFase3;
                SpikeChance = Boss.PorcentajeDeLanzarPinchosFase3;
                break;
        }
    }
}
