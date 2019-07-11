/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizing : MonoBehaviour {

    int n = 6;                          //Number of target objects in the game
    public GameObject target;
    public GameObject target_1;
    public GameObject target_2;
    public GameObject target_3;
    public GameObject target_4;
    public GameObject target_5;
    public GameObject target_6;
    GameObject[] array = new GameObject[7];

    public GameObject obj;

    AudioSource Fire;
    public GameObject selectedTarget;

    // Use this for initialization
    void Start () {

        array[0] = target;
        array[1] = target_1;
        array[2] = target_2;
        array[3] = target_3;
        array[1] = target_4;
        array[2] = target_5;
        array[3] = target_6;

       SetSource();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public GameObject getObject(){


        obj = array[Random.Range(0, n)];
        Debug.Log("Active Object" + obj.name);
        return (obj);
        
    }

    void SetSource()
    {
        obj = getObject();
        obj.GetComponent<AudioSource>().Play();
        obj.transform.gameObject.tag = "rightobject";
    }
  
}
*/