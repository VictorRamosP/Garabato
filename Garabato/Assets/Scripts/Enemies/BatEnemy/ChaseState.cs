using UnityEngine;

public class ChaseState : IState
{
    private BatEnemy enemy;
    private StateMachine stateMachine;

    public ChaseState(BatEnemy enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        //Debug.Log("Chase"); 
    }

    public void OnExit()
    {
        //Debug.Log("No Chase"); 
    }

    public void OnUpdate()
    {
        Vector2 direction = (enemy.Player.position - enemy.Bat.transform.position).normalized;
        Vector2 newPos = (Vector2)enemy.Bat.transform.position + direction * enemy.ChaseSpeed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(enemy.Bat.transform.position, direction, 0.5f, enemy.ObstacleLayer);
        if (hit.collider == null)
        {
            enemy.Bat.transform.position = newPos;
        }
    }

}
