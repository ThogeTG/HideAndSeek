using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public float timer;
    public float timeLeft;
    public Text text;
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            timer -= Time.deltaTime;
            string minutes = ((int)timer / 60).ToString();
            string seconds = (timer % 60).ToString("f2");
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
        finished = true;
        text.color = Color.red;
    }

}
