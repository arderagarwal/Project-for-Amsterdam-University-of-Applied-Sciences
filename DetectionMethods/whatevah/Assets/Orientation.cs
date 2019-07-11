using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(Player);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
