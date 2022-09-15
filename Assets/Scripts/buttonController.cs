using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonController : MonoBehaviour
{

    public void RefreshGame()
    {
        PlayerPrefs.SetInt("ScoreKey",0);
        PlayerPrefs.SetInt("LastTime", UI.Instance.MatchTime);
        SceneManager.LoadScene("SampleScene");
    }
}
