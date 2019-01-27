using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PowerupPickup : MonoBehaviour
{
    public string[] powerUps;       //Tai joku int-array
    Transform newForm;
    Movement move;

    public List<GameObject> shootItems = new List<GameObject>();
    public List<GameObject> currentShoots = new List<GameObject>();
    public GameObject kakka;

    bool shooter;
    Camera cam;

    //public GameObject[] powerupTexts;'
    public List<GameObject> powerupTexts = new List<GameObject>();
    GameObject myTexts;
    //
    GameObject shootText;
    GameObject speedText;
    GameObject maximizeText;
    GameObject minimizeText;
    GameObject transformText;

    int playerId;

    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    GameObject childBin;

    Rigidbody rigbod;

    bool powered;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Player1")
        {
            playerId = 1;
        }
        else if (gameObject.name == "Player2")
        {
            playerId = 2;
        }
        else if (gameObject.name == "Player3")
        {
            playerId = 3;
        }
        else if (gameObject.name == "Player4")
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

        rigbod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (shooter == true)
        {
            if (state.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y == ButtonState.Released)
            {
                int rand;
                //GameObject makeshiftBullet;

                rand = Random.Range(0, shootItems.Count);

                if (currentShoots.Count < 10)
                {
                    Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                    Instantiate(shootItems[rand], position, Quaternion.identity);
                    shootItems[rand].GetComponent<Rigidbody>().velocity = transform.forward * 5;

                    currentShoots.Add(shootItems[rand]);
                }
                else if (currentShoots.Count > 10)
                {
                    currentShoots[0].SetActive(false);
                    currentShoots[0].transform.position = transform.position;
                    currentShoots[0].SetActive(true);
                    currentShoots[0].GetComponent<Rigidbody>().velocity = transform.forward * 5;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Powerup" && powered == false)
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
                
                //rigbod.velocity = transform.up * 4;
                //GetComponent<CapsuleCollider>().enabled = false;
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
            powered = true;
        }
    }
}
