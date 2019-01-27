using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Door : MonoBehaviour
{
    Movement playerMovement;

    GamePadState state;

    Animator animator;

    public PlayerIndex playerIndex;
    GamePadState prevState;

    public bool isDoorOpen;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.root.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement = other.gameObject.GetComponent<Movement>();
            //playerMovement.DisplayText();

            if (isDoorOpen == false)
            {
                if (state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X== ButtonState.Released)
                {
                    Debug.Log("plöö");
                    animator.SetBool("IsOpen", true);
                    isDoorOpen = true;

                    //StartCoroutine()
                }
            }
            else if (isDoorOpen == true)
            {
                if (state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released)
                {
                    Debug.Log("plöö");
                    animator.SetBool("IsOpen", false);
                    isDoorOpen = false;
                }
            }

            /*
            if (isDoorOpen == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("plöö");
                    animator.SetBool("IsOpen", true);
                    isDoorOpen = true;
                }
            }
            
            else if (isDoorOpen == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("plöö");
                    animator.SetBool("IsOpen", false);
                    isDoorOpen = false;
                }
            }
            */
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
