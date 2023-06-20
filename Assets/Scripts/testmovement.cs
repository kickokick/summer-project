using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmovement : MonoBehaviour
{
    //Variables and stuff
    public float moveSpeed;
    public float jumpForce;
    public float drag;
    public float airDrag;
    public float crouchHeight;
    public float crouchDrag;
    public float sprintSpeed;
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

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

        }

        //Crouching
        if (Input.GetKey("c") && isOnGround)
        {   //Basically changes the objects size and then the rigidbody gravity does the rest, making the object fall to the ground
            transform.localScale = new Vector3(1f,crouchHeight,1f);
            rb.drag = crouchDrag;
        }
        else
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w") && (!(Input.GetKey("s")))))
        {
            rb.AddForce(moveDir * moveSpeed * sprintSpeed * Time.deltaTime, ForceMode.Impulse);
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
