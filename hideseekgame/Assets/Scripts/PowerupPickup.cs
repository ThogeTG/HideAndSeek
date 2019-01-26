using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public string[] powerUps;       //Tai joku int-array
    Transform newForm;
    Movement move;

    public GameObject[] shootItems;

    bool shooter;
    Camera cam;

    public GameObject[] powerupTexts;
    GameObject myTexts;
    //
    GameObject shootText;
    GameObject speedText;
    GameObject maximizeText;
    GameObject minimizeText;
    GameObject transformText;

    int playerId;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Player 1")
        {
            playerId = 1;
        }
        else if (gameObject.name == "Player 2")
        {
            playerId = 2;
        }
        else if (gameObject.name == "Player 3")
        {
            playerId = 3;
        }
        else if (gameObject.name == "Player 4")
        {
            playerId = 4;
        }

        newForm = transform.GetChild(1);
        move = GetComponent<Movement>();
        cam = transform.GetChild(0).GetComponent<Camera>();

        for (int i = 0; i < 4; i++)
        {
            powerupTexts[i] = GameObject.Find("Powerup Texts").transform.GetChild(i).gameObject;
        }

        myTexts = powerupTexts[playerId - 1];
        minimizeText = myTexts.transform.GetChild(0).gameObject;
        transformText = myTexts.transform.GetChild(1).gameObject;
        maximizeText = myTexts.transform.GetChild(2).gameObject;
        speedText = myTexts.transform.GetChild(3).gameObject;
        shootText = myTexts.transform.GetChild(4).gameObject;

        shootText.SetActive(false);
        speedText.SetActive(false);
        maximizeText.SetActive(false);
        minimizeText.SetActive(false);
        transformText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shooter == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                int rand;
                GameObject makeshiftBullet;

                rand = Random.Range(0, shootItems.Length);

                makeshiftBullet = Instantiate(shootItems[rand], transform);
                makeshiftBullet.GetComponent<Rigidbody>().velocity =  cam.transform.forward * 50;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            int rand;

            rand = Random.Range(0, powerUps.Length);

            if (rand == 0)
            {
                //Become SMALL
                transform.localScale /= 3;
                minimizeText.SetActive(true);

            }
            else if (rand == 1)
            {
                //Become CAMOUFLAGED
                transform.localScale *= 3;
                maximizeText.SetActive(true);
            }
            else if (rand == 2)
            {
                //Become TRANSFORMED
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
                newForm.gameObject.SetActive(true);
                transformText.SetActive(true);
            }
            else if (rand == 3)
            {
                //Become FAST
                move.walkSpeed *= 2;
                move.runSpeed *= 2;
                move.crawlSpeed *= 2;
                speedText.SetActive(true);
            }
            else if (rand == 4)
            {
                //Become SHOOTER
                shooter = true;
                shootText.SetActive(true);
            }

            print(rand);
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
