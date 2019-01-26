using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 1;
    public float smoothing = 5;
    Movement movement;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.gameObject;
        movement = player.GetComponent<Movement>();
        //GetComponent<Camera>().cullingMask
    }

    // Update is called once per frame
    void Update()
    {
        var middle = new Vector2(Input.GetAxisRaw("CamHMove"+movement.player), Input.GetAxisRaw("CamVMove"+movement.player));

        middle = Vector2.Scale(middle, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, middle.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, middle.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, -50, 50);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}
