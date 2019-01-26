﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHold : MonoBehaviour
{
    Transform originParent;
    GameObject heldObject;
    Vector3 holdPosition;

    Camera cam;

    bool holding;

    public float distance = 1.5f;
    public Vector3 height = new Vector3(0, 0.15f, 0);
    public float throwPower = 10;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heldObject != null)
        {
            if (holding == true)
            {
                heldObject.transform.rotation = gameObject.transform.GetChild(0).rotation;
                heldObject.transform.position = gameObject.transform.GetChild(0).position + (heldObject.transform.rotation * (Vector3.forward - height) * distance);

                if (Input.GetMouseButtonDown(1))
                {
                    holding = false;
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    heldObject.GetComponent<Rigidbody>().velocity = cam.transform.forward * throwPower;

                    holding = false;
                }
            }
            else
            {
                heldObject.transform.parent = originParent;
                heldObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Holdable" && Input.GetMouseButtonDown(0))
        {
            Hold(other.gameObject.transform.parent.gameObject);
        }
    }

    void Hold(GameObject item)
    {
        heldObject = item;
        originParent = heldObject.transform.parent;
        heldObject.transform.GetChild(0).gameObject.SetActive(false);
        heldObject.transform.parent = this.transform.GetChild(0);

        holding = true;
    }
}