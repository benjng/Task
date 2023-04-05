using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    private string currentState;

    // TODO: Setup corresponding duration from each state to each state
    private float transitionDuration = 0.1f;

    //Animation States
    const string PLAYER_JUMP = "Jumping";
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_FAST_RUN = "Fast Run";
    const string PLAYER_CROUCHED_WALK = "Crouched Walking";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Change animations with transition
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            //print("In same state");
            return;
        }

        if (animator.IsInTransition(0))
        {
            //print("In Transition");
            return;
        }

        //play the animation with transition
        animator.CrossFade(newState, transitionDuration);
        //print("Transiting from " + currentState + " to " + newState);

        //reasign the current state
        currentState = newState;
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool shiftPressed = Input.GetKey("left shift");
        bool spacePressed = Input.GetButtonDown("Jump");

        // No key pressed case
        if (!Input.anyKey)
        {
            ChangeAnimationState(PLAYER_IDLE);
            return;
        }

        // Check expected inputs
        if (forwardPressed || backwardPressed || leftPressed || rightPressed)
        {
            if (shiftPressed)
            {
                ChangeAnimationState(PLAYER_FAST_RUN);
                return;
            }
            ChangeAnimationState(PLAYER_CROUCHED_WALK);
            return;
        }

        if (spacePressed)
        {
            //animator.SetBool(isJumpingHash, true);
            ChangeAnimationState(PLAYER_JUMP);
            return;
        }

        // Any other unexpected key combinations
        ChangeAnimationState(PLAYER_IDLE);
    }
}
