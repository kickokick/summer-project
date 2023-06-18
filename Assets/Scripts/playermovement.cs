using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        ResetJump();
        readyToJump = true;
    }

    

    // Update is called once per frame
    private void Update()
    {
        //Checks if player is grounded or touching the ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        MyInput();

        SpeedControl();
        
        //drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        

    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        //translated input or soemthing
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void MovePlayer()
    {
        //calculates movement dir
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    //limits player speed
    //IMPORTANT****WILL HAVE TO CHANGE TO FIT THE GAMEPLAY FOR HORROR GAME****
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limits velocity if velocity is over intended velocity
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset y velocity so that you alway jump same height
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Apply force, impulse does once
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
