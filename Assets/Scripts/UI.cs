using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text ScoreText;
    public Text TimeText;
    public int MatchTime = 120;
    public float StartTime = 0;
    int Score = 0;
    private bool MatchActive = false;


    public static UI Instance;
    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        TimeText.text = "Time" + MatchTime;
        StartTime = Time.time;
        MatchActive = true;
    }
    public void ScorePlus()
    {
        if(MatchActive)
        {
            Score++;
            ScoreText.text = "Score:" + Score.ToString();
        }
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Time.time - StartTime < MatchTime)
        {
            TimeText.text = "Time:" + (MatchTime - (Time.time - StartTime));
        }
        else
        {
            MatchActive = false;
        }
    }
   
}
