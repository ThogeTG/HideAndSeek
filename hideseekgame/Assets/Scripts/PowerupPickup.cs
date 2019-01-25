using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public string[] powerUps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            int rand;

            rand = Random.Range(0, powerUps.Length);

            if (rand == 1)
            {

            }
            else if (rand == 2)
            {

            }

            Destroy(other.gameObject);
        }
    }
}
