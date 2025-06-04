using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapState : IState
{
    private PlayerMachine player;
    public MapState(PlayerMachine _player) {
        player = _player;
    }
    public void OnEnter()
    {
        player.rb.constraints = RigidbodyConstraints2D.FreezePosition;
        player.transform.SetParent(GameObject.FindGameObjectWithTag("Map").transform);
    }
    public void OnUpdate()
    {
        player.transform.rotation = Quaternion.identity;
    }
    public void OnExit()
    {
        player.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
