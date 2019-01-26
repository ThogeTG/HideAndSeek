using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public bool inputsEnabled;
    public bool hideTime;
    public float timer;
    public Text text;
    private bool finished;
    public Camera[] hiderCams;
    int round;
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
        if(timer <= 0)
        {
            Finish();
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

        }
    }

    IEnumerator look()
    {
        yield return new WaitForSeconds(3);
        inputsEnabled = true;
        text.color = Color.green;
        timer = 100;
        finished = false;
    }

}
