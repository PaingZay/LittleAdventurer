using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Run : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponent<PlayerVFXManager>() != null)
            animator.GetComponent<PlayerVFXManager>().Update_FootStep(true);
        /*

        1. The OnStateEnter method in the Player_Run script is triggered since the animation state was entered.
        2. Inside OnStateEnter, the script checks if the player object has a PlayerVFXManager component attached to it.
        3. If the check returns true, it means that the player object has a PlayerVFXManager component. This component contains the Update_FootStep method, which can handle the footstep visual effects.
        4. The script then calls Update_FootStep(true) to signal the PlayerVFXManager to play the footstep visual effects.
 
        */
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<PlayerVFXManager>() != null)
            animator.GetComponent<PlayerVFXManager>().Update_FootStep(false);

        /*
         
        1. This is reverse action of OnStateEnter. Check. If it is true then stop OnStateExit

        */
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
