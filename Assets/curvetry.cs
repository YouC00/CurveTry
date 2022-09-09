using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;

public class curvetry : MonoBehaviour
{

    private Vector3 startPos; //mouse slide movement start pos
    private Vector3 endPos; //mouse slide movement end pos
    public float zDistance = 30.0f;//z distance
    private bool isThrown;
    private float startTime, endTime;

    void Start()
    {
        isThrown = false;
    }
    void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition * -1.0f;
        mousePos.z = zDistance; //add z distance

        startPos = Camera.main.ScreenToWorldPoint(mousePos);
        startTime = Time.time;
        //Print start Pos for debugging
        Debug.Log(startPos);
    }
    void OnMouseUp()
    {
        Vector3 mousePos = Input.mousePosition * -1.0f;
        mousePos.z = zDistance; //add z distance

        // convert mouse position to world position
        endPos = Camera.main.ScreenToWorldPoint(mousePos);
        endPos.z = Camera.main.nearClipPlane; //removing this breaks stuff,no idea why though
        endTime = Time.time;
        //Print start Pos for debugging
        Debug.Log(endPos);

        Vector3 throwDir = (startPos - endPos).normalized;//get throw direction based on start and end pos

        this.gameObject.GetComponent<Rigidbody>().AddForce(throwDir * (startPos - endPos).sqrMagnitude);//add force to throw direction*magnitude

        isThrown = true;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0); // Reset scene on pressing R
        }
       
    }
    void FixedUpdate()
    {
        //this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(HorizontalInput, 0.0f, verticalInput) * speed);
        float duration = endTime - startTime;
       
    }
}