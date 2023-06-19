using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float drag;
    public float airDrag;
    public bool isOnGround = true;
    private float horizontalInput;
    private float forwardInput;
    public Transform orientation;
    private Rigidbody rb;  
    // Start is called before the first frame update
    void Start()
    {
        // assigns rigidbody to rb
        rb = GetComponent<Rigidbody>();
        // so bean man dont fall over
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // gets players input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Camera direction
        Vector3 camForward = orientation.forward;
        Vector3 camRight = orientation.right;

        // So you cant start flying
        camForward.y = 0;
        camRight.y = 0;

        // Creating rel cam direction
        Vector3 forwardRelative = forwardInput * camForward;
        Vector3 rightRelative = horizontalInput * camRight;

        Vector3 moveDir = forwardRelative + rightRelative;

        // move the player  which direction on axis | speed | what input buttons
        
        // THIS WORKS WOOO HOOO AM SO SMART
        rb.AddForce(moveDir * moveSpeed * Time.deltaTime, ForceMode.Impulse);

        //THESE ONES WERE NOT WORKING
        //transform.Translate(moveDir * Time.deltaTime * moveSpeed * forwardInput);
        //transform.Translate(moveDir * Time.deltaTime * moveSpeed * horizontalInput);

        //add drag
        if (isOnGround)
        {
            rb.drag = drag;
        }
        else
        {
            rb.drag = airDrag;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

        }

    }
  
    // Pretty simple, checks for a collision with an object that has the ground tag, if collided, then resets the is on ground tag
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
        }
    }
}
