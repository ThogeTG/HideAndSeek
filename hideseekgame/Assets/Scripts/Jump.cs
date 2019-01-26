using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpPower = 5;
    bool grounded;

    float groundColDistance = 2;

    Rigidbody rigBod;

    // Start is called before the first frame update
    void Start()
    {
        rigBod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                //Kokeile tätä uudelleen sitten kun saat liikkumisscriptin takaisin
                //JA KORJAA CROUCH
                //nii ja lisää referenssi groundediin movementiin
                //print("Kakka!");
                //rigBod.velocity = transform.up * jumpPower;
                //transform.Translate(Vector3.up);
            }*/
        }
    }
}
