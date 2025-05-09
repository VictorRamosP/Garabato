using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public StateMachine stateMachine;
    public PlayerJumper playerJumper;
    void Start()
    {
        playerJumper = GetComponent<PlayerJumper>();
        
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.OnUpdate();    
    }
}
