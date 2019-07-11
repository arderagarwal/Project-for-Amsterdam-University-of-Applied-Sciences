using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class NewBehaviourScript : MonoBehaviour
{


    public SteamVR_Action_Vibration hapticAction;
    public SteamVR_Action_Boolean trackpadAction;
    public GameObject hand;

    int counter = -2;    //Counting selected objects
    int wcounter = 0;
    public SteamVR_Input_Sources source;
    private float status;

    private float defaultTimeInterval = 0.5f;
    private float triggerStatus = 0;
    public GameObject correcttarget;                //Correct target
   
   
    int correctHit;
    int incorrectHit;
    int n = 6;

    //Game mode control variables
    int method = 1;             //1 for sound detection 2 for vibration
    bool vibration = false;     //turns vibration of initially
   
   //Number of target objects in the game
   
    public GameObject[] cubes;                      //Creating dynamic array
    GameObject obj;

    AudioSource Fire;
    public GameObject selectedTarget;
    Canvas canvas;
    public Light lt;
    RaycastHit hit;     //Info about the object that is hit
    public GameObject target;
    private float start_time;
    // Start is called before the first frame update
    public float angleForward;      //Defintion of te forward ange
    public float viewAngle = 20;    //Definition of the angle
    public float viewRadius = 5;    //Definition of the radius of the view point
    public float intensity = 0;
    Renderer currRend;

    float timer = 0f;
   // Text timerText;
    

    public Text displayTimer;
    public Text wrongCounter;
    void Start()
    {

        StartCoroutine(TimedUpdateRoutine());   //Activates time update routine
        SetSource();                            
    }

    // Update is called once per frame
    void Update()
    {  if (counter == -1) {
            wcounter = 0;
        }

        if(counter>=0 && counter < 10)
        {
            timer += Time.deltaTime;

        }
        if(counter == 10)
        {
            changecolor3();
        }

        wrongCounter.text = "Errors : " + wcounter.ToString();

        //timerText = displayTimer.GetComponent<Text>();
        displayTimer.text = "Timer : " + timer.ToString();


        if (trackpadAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
             if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
             {
                 Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);


                 if (hit.collider.tag == "rightobject")
                 {
                     changecolor();
                    currRend = hit.collider.gameObject.GetComponent<Renderer>();
                }
                else
                {
                    Debug.Log("wrong selected");
                    currRend = hit.collider.gameObject.GetComponent<Renderer>();
                    changecolor2();
                    
                }
               
             }
             else
             {
                 Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
             }

            triggerStatus = 1; 

        }

        if (trackpadAction.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            // Does the ray intersect any collider excluding the player layer (returns bool)
            triggerStatus = 0;
        }

        //If target that is hit is the correct target => increment + restart
        //If target is incorrect => increment and restart

    }

    void SetSource()
    {
        //obj = getObject();
        obj = cubes[Random.Range(0, cubes.Length)];
        if (method == 1) {
            Debug.Log(obj.name);
            obj.GetComponent<AudioSource>().Play();
        }
        else{
            vibration = true;
        }
        
        obj.transform.gameObject.tag = "rightobject";
    }

    public void changecolor()
    {
        Renderer rend = obj.GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.green);
        counter = counter + 1;
        Debug.Log(counter);
        Invoke("xyz", 2);
        vibration = false;
        obj.GetComponent<AudioSource>().Stop();
        obj.transform.gameObject.tag = "wrongobject";
    }
    public void changecolor2()
    {
        
        currRend.material.SetColor("_Color", Color.red);
        wcounter = wcounter + 1;
        Debug.Log(wcounter);
        Invoke("xyz", 2);
        vibration = false;
        obj.GetComponent<AudioSource>().Stop();
        obj.transform.gameObject.tag = "wrongobject";
    }
    public void changecolor3()
    {

        //lt = GetComponent<Light>();
        lt.color = Color.red;
        
    }
    public void xyz()
    {
        Renderer rend = obj.GetComponent<Renderer>();
        currRend.material.SetColor("_Color", Color.white);
        rend.material.SetColor("_Color", Color.white);
        SetSource();
    }
   

    public float AngleCalc()
    {
        //Calcualtes angle between straight line connecting two objects and forward direction of the hand
        angleForward = Vector3.Angle(this.transform.forward, this.transform.position - obj.transform.position);
        //Debug.Log(angleForward);
        return angleForward;

    }

    public float IntensityGet()
    {

        switch (AngleCalc())
        {
            case float n when (n > 160):
                intensity = 0.1f;
                break;

            case float n when (n > 140 && n <= 160):
                intensity = 0.2f;
                break;

            case float n when (n > 120 && n <= 140):
                intensity = 0.3f;
                break;

            case float n when (n > 100 && n <= 120):
                intensity = 0.4f;
                break;

            case float n when (n > 80 && n <= 100):
                intensity = 0.5f;
                break;

            case float n when (n > 60 && n <= 80):
                intensity = 0.6f;
                break;

            case float n when (n > 40 && n <= 60):
                intensity = 0.7f;
                break;

            case float n when (n > 20 && n <= 40):
                intensity = 0.8f;
                break;

            case float n when (n < 20f):
                intensity = 0.9f;
                break;

        }
        return (intensity);

    }
    private void Pulse()
    {
        //Pulses vibration with fixed parameters   
        hapticAction.Execute(0, 0.05f, 200, 0.9f, SteamVR_Input_Sources.RightHand);
        //Debug.Log(defaultTimeInterval);
        //float duration, float frequency,float amplitude, SteamVR_Input_Sources source
    }


    IEnumerator TimedUpdateRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(defaultTimeInterval);   //Sets new carantine value
            if (vibration) {
                defaultTimeInterval = IntensityGet();                   //Corrects intensity depending on the angle           
                Pulse();                                              //Pulses vibration
            }
        }
    }



}
