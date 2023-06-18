using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Variables
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Locks Cursor so no movey movey and hides it just like seths parents
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //dunno why but need it so keep it
        yRotation += mouseX;

        xRotation -= mouseY;
        //Limits how far look up and down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  

        //rotate camera (so it moves WOWO)
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
