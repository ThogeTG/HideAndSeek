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

    public string whichDoor;

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
                    animator.SetBool(whichDoor, true);
                    StartCoroutine(WaitTrue());
                    //isDoorOpen = true;
                }
            }
            else if (isDoorOpen == true)
            {
                if (state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released)
                {
                    Debug.Log("plöö");
                    animator.SetBool(whichDoor, false);
                    StartCoroutine(WaitFalse());   
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

    IEnumerator WaitTrue()
    {
        yield return new WaitForSeconds(1);
        isDoorOpen = true;
    }

    IEnumerator WaitFalse()
    {
        yield return new WaitForSeconds(1);
        isDoorOpen = false;
    }
}
