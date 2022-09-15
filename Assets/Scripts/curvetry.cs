using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class curvetry : MonoBehaviour
{
    
    [SerializeField] GameObject leftOb;
    [SerializeField] GameObject rightOb;
    [SerializeField] GameObject GoalOb;
    [SerializeField] GameObject PlaneOb;
    Vector3 startPos; //mouse slide movement start pos
    Vector3 endPos; //mouse slide movement end pos
    Vector3 otherPos; //mouse slide movement end pos
    public float zDistance = 30.0f;//z distance

    List<Vector3> listPoints = new List<Vector3>();
    List<Vector3> list = new List<Vector3>();

    
    Vector3 tempV1; 
    Vector3 tempV2;
    Vector3 tempV3;
       
    float waitTime = 0;

    bool isTimeActive = false;
    bool isLeftCurve;
    
    // uL isminde tanımladığım UI classı ile oradaki variablelara müdehale edebildik.
    // UI uL = new UI();
    // uL isminde tanımladığım UI classı ile oradaki variablelara müdehale edebildik.

    
        

       /* COMMENT
       
        else if (UI.Instance.StartTime > 5)
        {
            SceneManager.LoadScene("SampleScene");
        }

        // uL isminde tanımladığım UI classı ile oradaki variablelara müdehale edebildik.

        else if (uL.StartTime > 5)
        {
            SceneManager.LoadScene("SampleScene");
        }
        // uL isminde tanımladığım UI classı ile oradaki variablelara müdehale edebildik.

        */
    

    void Start()
    {
        //Score = 0;
        
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

        this.gameObject.GetComponent<Rigidbody>().AddForce(throwDir * 5f * (startPos - endPos + tempV2).sqrMagnitude);//add force to throw direction*magnitude

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

            GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(3f, transform.position.z), GoalOb.transform.position);
            GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(7f, transform.position.y), PlaneOb.transform.position);

            if (tempV1.x < tempV2.x && tempV2.x > tempV3.x)
            {
                GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(8f, transform.position.y), leftOb.transform.position);
                
                isLeftCurve = false;
            }
            else if (tempV1.x > tempV2.x && tempV2.x < tempV3.x)
            {
                GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(- 8f, transform.position.y), rightOb.transform.position); 
               
                isLeftCurve = true;    
            } 
        }
    }   
}