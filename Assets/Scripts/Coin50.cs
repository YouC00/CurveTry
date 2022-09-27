using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin50 : MonoBehaviour
{
    public static UI Instance;
    // Start is called before the first frame update


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "ball")
        {
            Destroy(this.gameObject);
            UI.Instance.ScoreController(30);
        }

    }
}
