using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float jumpForce;
    //public Rigidbody theRB;
    public CharacterController controller;
    public float gravityScale;

    private Vector3 moveDirection;

    // Use this for initialization
    void Start () {
        //theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        //theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y,Input.GetAxis("Vertical") * moveSpeed);

        //if (Input.GetButtonDown("Jump"))
        //{
        //    theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        //}

        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }
        
        moveDirection.y = moveDirection.y + Physics.gravity.y * gravityScale * Time.deltaTime;

        //time.deltaTime makes sure movement doesn't occur every frame, so that higher fps causes faster movement
        controller.Move(moveDirection * Time.deltaTime);
    }
}
