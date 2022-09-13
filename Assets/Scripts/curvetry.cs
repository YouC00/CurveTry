using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class curvetry : MonoBehaviour
{

    [SerializeField] GameObject leftOb;
    [SerializeField] GameObject rightOb;

    Vector3 startPos; //mouse slide movement start pos
    Vector3 endPos; //mouse slide movement end pos
    Vector3 otherPos; //mouse slide movement end pos
    public float zDistance = 25.0f;//z distance

    List<Vector3> listPoints = new List<Vector3>();
    List<Vector3> list = new List<Vector3>();

    
    Vector3 tempV1; 
    Vector3 tempV2;
    Vector3 tempV3;

    float waitTime = 0;

    bool isTimeActive = false;
    bool isLeftCurve;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("ANKARAMESSİANKARAMESSİGOLGOLGOLGOLGOL");
        }

        else if (other.gameObject.tag == "KillZone")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    void Start()
    {

    }

    void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition * -1.0f;
        mousePos.z = zDistance; //add z distance

        startPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Print start Pos for debugging
        isTimeActive = false;
    }
    IEnumerator  HoldTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isTimeActive = false;
    }
    void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition * -1.0f;
        mousePos.z = zDistance; //add z distance

        otherPos = Camera.main.ScreenToWorldPoint(mousePos);
        otherPos.z = Camera.main.nearClipPlane; //removing this breaks stuff,no idea why though

        listPoints.Add(otherPos);
    }

    void OnMouseUp()
    {
       
        Vector3 mousePos = Input.mousePosition * -1.0f;
        mousePos.z = zDistance; //add z distance

        // convert mouse position to world position
        endPos = Camera.main.ScreenToWorldPoint(mousePos);
        endPos.z = Camera.main.nearClipPlane; //removing this breaks stuff,no idea why though
        //Print start Pos for debugging

        Vector3 throwDir = (startPos - endPos).normalized;//get throw direction based on start and end pos

        this.gameObject.GetComponent<Rigidbody>().AddForce(throwDir * 3f * (startPos - endPos).sqrMagnitude);//add force to throw direction*magnitude

        tempV1 = startPos;
        tempV2 = listPoints[(int)(listPoints.Count / 2)];
        tempV3 = endPos;

        isTimeActive = true;
    }
    
    void Update()
    {

        if (isTimeActive)
        {
            StartCoroutine(HoldTime(1.0f));


            if (tempV1.x < tempV2.x && tempV2.x > tempV3.x)
            {
                GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(5f, transform.position.y), leftOb.transform.position);
                
                isLeftCurve = false;
            }
            else if (tempV1.x > tempV2.x && tempV2.x < tempV3.x)
            {
                GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(- 5f, transform.position.y), rightOb.transform.position); 
               
                isLeftCurve = true;    
            } 
        }
    }   
}