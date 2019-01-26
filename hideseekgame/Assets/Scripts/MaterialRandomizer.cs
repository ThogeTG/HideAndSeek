using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomizer : MonoBehaviour
{
    public Material[] materials;
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        int rand;

        foreach(GameObject obj in objects)
        {
            rand = Random.Range(0, materials.Length);

            obj.GetComponent<MeshRenderer>().material = materials[rand];
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int rand;

            foreach (GameObject obj in objects)
            {
                rand = Random.Range(0, materials.Length);

                obj.GetComponent<MeshRenderer>().material = materials[rand];
            }
        }
    }
}
