using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorFunc : MonoBehaviour
{
    public Transform doorTransform;
    private float x = 0;
    void OnTriggerStay(Collider col)
    {
        if ((col.tag == "Door") && (Input.GetKey("e")) && x == 0)
        {
            doorTransform.position += new Vector3(5f, 0f, 0f);
            print("SHITS workin");
            x += 1;
        }
    }
}
