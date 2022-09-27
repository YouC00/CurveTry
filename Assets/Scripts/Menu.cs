using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayButton()
    {    
        PlayerPrefs.SetInt("ScoreKey", 0);
        PlayerPrefs.SetInt("LastTime", UI.Instance.MatchTime);
        SceneManager.LoadScene("Antreman");

    }
    public void QuitButton()    
    {
        Application.Quit();
    }

}
