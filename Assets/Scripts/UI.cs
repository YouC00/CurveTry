using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text ScoreText;
    public Text TimeText;
    public int MatchTime = 120;
    public float StartTime = 0;
    public float CurrentTime;
    int Score;
    private bool MatchActive = false;
    public UI Controller;
    int lastTime;


    public static UI Instance;
    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartTime = Time.time;
        MatchActive = true;
        ScoreText.text = "Score: " + PlayerPrefs.GetInt("ScoreKey").ToString();
        TimeText.text = (PlayerPrefs.GetInt("LastTime") != MatchTime) ? PlayerPrefs.GetInt("LastTime").ToString() : MatchTime.ToString(); 
    }
    public void ScorePlus()
    {
        if (MatchActive)
        {
            Score = PlayerPrefs.GetInt("ScoreKey");
            Score++;
            PlayerPrefs.SetInt("ScoreKey", Score);
            ScoreText.text = "Score: " + Score.ToString();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("GOL");
            Controller.ScorePlus();

        }
        else if (other.gameObject.tag == "KillZone")
        {
            //PlayerPrefs.GetInt("ScoreKey");
            string seconds = TimeText.text.ToString();
            string[] secondsArr = seconds.Split(' ');
            lastTime = Convert.ToInt32(secondsArr[1]) ;
            PlayerPrefs.SetInt("LastTime", lastTime);
            SceneManager.LoadScene("SampleScene");
        }
    }
    // Update is called once per frame
    void Update()
    {
        AktiveTime();
    }

    private void AktiveTime()
    {
        if (Time.time - StartTime < PlayerPrefs.GetInt("LastTime"))
        {
            TimeText.text = "Time: " + (int)(PlayerPrefs.GetInt("LastTime") - (Time.time - StartTime));
        }
        else
        {
            MatchActive = false;
        }
    }
}
