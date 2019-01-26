using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Jump : MonoBehaviour
{
    public float jumpPower = 5;
    bool grounded;

    float groundColDistance = 2;

    Rigidbody rigBod;

    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Start is called before the first frame update
    void Start()
    {
        rigBod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        RaycastHit ray;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out ray, groundColDistance))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * ray.distance, Color.red);
            //Debug.Log("Hit!");

            grounded = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * ray.distance, Color.red);
            //Debug.Log("No hit...");
            grounded = false;
        }

        if (grounded == true)
        {
            if (state.Buttons.A == ButtonState.Pressed)
            {
                //JA KORJAA CROUCH
                //nii ja lisää referenssi groundediin movementiin (crouch)
                rigBod.velocity = transform.up * jumpPower;
            }
        }
    }
}
