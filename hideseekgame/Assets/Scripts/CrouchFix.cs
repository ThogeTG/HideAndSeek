using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<CapsuleCollider>().height /= 2;

        }
    }
}
