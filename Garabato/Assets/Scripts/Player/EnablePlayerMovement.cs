using UnityEngine;

public class EnablePlayerMovement : StateMachineBehaviour
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        PlayerMove playerMove = animator.GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            playerMove.canMove = true;
        }
    }
}

