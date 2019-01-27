using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public List<string> points;
    public bool inputsEnabled;
    public bool hideTime;
    public float timer;
    public Text text;
    private bool finished;
    public Camera[] hiderCams;
    public int round;
    // Start is called before the first frame update
    void Start()
    {
        inputsEnabled = true;
        hideTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            timer -= Time.deltaTime;
            string minutes = ((int)timer / 60).ToString();
            string seconds = (timer % 60).ToString("f0");
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            text.text = minutes + ":" + seconds;
        }
        if(timer <= 0 && !finished)
        {
            Finish();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

    }
    public void Finish()
    {
        round++;
        if (round == 1)
        {
            finished = true;
            text.color = Color.red;
            foreach (Camera cam in hiderCams)
            {
                cam.clearFlags = CameraClearFlags.SolidColor;
                cam.backgroundColor = Color.black;
                cam.cullingMask = (1 << LayerMask.NameToLayer("nothing")) | (1 << LayerMask.NameToLayer("nothing"));
                cam.fieldOfView = 0;

            }
            inputsEnabled = false;
            StartCoroutine("look");
        }
        else if(round == 2)
        {
            GameOver();
        }
    }

    IEnumerator look()
    {
        yield return new WaitForSeconds(3);
        inputsEnabled = true;
        text.color = Color.green;
        timer = 90;
        finished = false;
    }
    public void GameOver()
    {
        
        finished = true;
        points.Add("Player 1 \n");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if(player.GetComponent<Movement>().enabled == true && player.GetComponent<Movement>().player != 1)
            {
                points.Add(("Player " + player.GetComponent<Movement>().player.ToString() + "\n"));
                Camera cam = player.transform.GetComponentInChildren<Camera>();
                cam.clearFlags = CameraClearFlags.Skybox;
                cam.backgroundColor = Color.blue;
                cam.cullingMask = -1;
                cam.fieldOfView = 60;
            }
            player.GetComponent<Movement>().enabled = false;
        }
        text.text = "Win order: \n"  +points[3] + points[2] + points[1] + points[0];
    }

}
