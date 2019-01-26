using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;


public class Movement : MonoBehaviour
{
    public int player;
    public PlayerIndex playerIndex;
    float realSpeed;
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float crawlSpeed = 2.5f;

    
    float translation;
    float strafe;

    bool facingWall;
    bool facingObject;

    Vector2 velocity;

    Rigidbody rb;

    Transform camera;

    bool lightOn;

    Rigidbody rigBod;

    float originHeight;
    float crouchHeight;
    bool crouching;

    float originRot;
    float rightPeekRot;
    float leftPeekRot;

    bool peeking;

    Animation leftPeek;

    public Text doorText;

    GamePadState state;
    GamePadState prevState;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = transform.GetChild(0);
        rigBod = GetComponent<Rigidbody>();

        originHeight = transform.position.y;
        crouchHeight = originHeight - 1;

        originRot = 0;
        rightPeekRot = -1;
        leftPeekRot = 1;

        leftPeek = GetComponent<Animation>();
    }

    private void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!crouching)
        {
            originHeight = transform.position.y;
        }

        Move();

        Crouch();

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Move()
    {
        if (state.Buttons.RightShoulder == ButtonState.Pressed)
        {
            realSpeed = runSpeed;
        }
        else if (state.Buttons.B == ButtonState.Pressed)
        {
            realSpeed = crawlSpeed;
        }
        else
        {
            realSpeed = walkSpeed;
        }
        

        translation = state.ThumbSticks.Left.Y * realSpeed;
        strafe = state.ThumbSticks.Left.X * realSpeed;

        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        rigBod.MovePosition(transform.position + (transform.forward * translation + transform.right * strafe));
    }

    void Crouch()
    {
        Vector3 pos = transform.position;

        if (state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released)
        {
            if (!crouching)
            {
                crouching = true;
            }
            else
            {
                crouching = false;
            }
        }

        if (crouching == true)
        {
            GetComponent<CapsuleCollider>().height = 1.1f;
        }
        else
        {
            GetComponent<CapsuleCollider>().height = 2;
        }

    }

    private void OnTriggerStay(Collider other)
    {

    }

    public void DisplayText()
    {
        doorText.text = "Press E";
    }
}
