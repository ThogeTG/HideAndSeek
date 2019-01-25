using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
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

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        Crouch();

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            realSpeed = crawlSpeed;
        }
        else
        {
            realSpeed = walkSpeed;
        }
        

        translation = Input.GetAxis("Vertical") * realSpeed;
        strafe = Input.GetAxis("Horizontal") * realSpeed;

        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        rigBod.MovePosition(transform.position + (transform.forward * translation + transform.right * strafe));
    }

    void Crouch()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.C))
        {
            crouching = true;
        }
        else
        {
            crouching = false;
        }

        if (crouching == true)
        {
            pos.y = Mathf.Lerp(transform.position.y, crouchHeight, .3f);
            transform.position = pos;
        }
        else
        {
            pos.y = Mathf.Lerp(transform.position.y, originHeight, .3f);
            transform.position = pos;
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
