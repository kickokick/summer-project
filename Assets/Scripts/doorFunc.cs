using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorFunc : MonoBehaviour
{
    public Transform doorTransform;
    private bool doorClosed = true;
    private bool isTrigger = false;
    //Creates flags instead
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Door")
        {
            isTrigger = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Door")
        {
            isTrigger = false;
        }
    }
    //Needed to be able to use GetKeyDown
    void Update()
    {
        if ((isTrigger) && (Input.GetKeyDown("e")) && doorClosed)
        {
            print("Door opened");
            doorTransform.position += new Vector3(5f, 0f, 0f);
            doorClosed = false;
        }
        else if ((isTrigger) && (Input.GetKeyDown("e") && (!doorClosed)))
        {
            print("Door closed");
            doorTransform.position += new Vector3(-5f, 0f, 0f);
            doorClosed = true;
        }
    }
    //Doesn't work
/*     void OnTriggerStay(Collider col)
    {
        if ((col.tag == "Door") && (Input.GetKey("e")) && doorClosed == true)
        {
            doorTransform.position += new Vector3(5f, 0f, 0f);
            print("Opened working");
            doorClosed = false;
        }
        if ((col.tag == "Door") && (Input.GetKey("e")) && doorClosed == false)
        {
            doorTransform.position += new Vector3(-5f, 0f, 0f);
            print("CLOSED Working");
            doorClosed = true;
        }
    } */
}
