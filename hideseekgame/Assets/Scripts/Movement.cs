﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;


public class Movement : MonoBehaviour
{
    Manager manager;
    public int player;
    public PlayerIndex playerIndex;
    float realSpeed;
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float crawlSpeed = 2.5f;

    bool moving;
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

    public Text caughtText;



    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
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
        if(manager.round == 1)
        {
            if(state.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released && !moving)
            {
                moving = true;
                Camera cam = transform.GetComponentInChildren<Camera>();
                cam.clearFlags = CameraClearFlags.Skybox;
                cam.backgroundColor = Color.blue;
                cam.cullingMask = -1;
                cam.fieldOfView = 60;
            }
        }
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
        if (state.Buttons.RightShoulder == ButtonState.Pressed && manager.inputsEnabled)
        {
            realSpeed = runSpeed;
        }
        else if (state.Buttons.B == ButtonState.Pressed && manager.inputsEnabled)
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

        if (state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released && manager.inputsEnabled)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (manager.round == 1 && collision.transform.tag == "Player" && player == 1 && !manager.points.Contains("Player " + collision.gameObject.GetComponent<Movement>().player.ToString()+ "\n"))
        {
            manager.points.Add("Player " + collision.gameObject.GetComponent<Movement>().player.ToString() + "\n");
            collision.gameObject.GetComponent<Movement>().enabled = false;

            collision.gameObject.GetComponent<Movement>().caughtText.text = "Youch!! You got caught!";
            collision.gameObject.GetComponent<Movement>().caughtText.gameObject.SetActive(true);

            Camera cam = collision.gameObject.transform.GetComponentInChildren<Camera>();
            cam.clearFlags = CameraClearFlags.Skybox;
            cam.backgroundColor = Color.blue;
            cam.cullingMask = -1;
            cam.fieldOfView = 60;

        }
    }

    public void DisplayText()
    {
        doorText.text = "Press E";
    }
}
