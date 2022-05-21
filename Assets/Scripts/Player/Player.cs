using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject groundCheck;
    public float moveSpeed;
    public float jumpSpeed;
    public float acceleration;

    Rigidbody rigidBody;
    float moveX;
    int moveDir;
    bool jumped;
    bool grounded;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.transform.position, .1f, LayerMask.GetMask("Terrain"));
        moveDir = (int)Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumped = true;
        }
    }

    void FixedUpdate()
    {
        if (moveDir != 0)
            moveX = Mathf.MoveTowards(moveX, moveDir * moveSpeed, Time.deltaTime * acceleration);
        else
            moveX = Mathf.MoveTowards(moveX, moveDir * moveSpeed, Time.deltaTime * acceleration * 2f);
        rigidBody.velocity = new Vector3(moveX, rigidBody.velocity.y, 0);

        if (jumped)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0);
            jumped = false;
        }

        if (rigidBody.velocity.y < .75f * jumpSpeed || !Input.GetButton("Jump"))
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * 5f;
        }
        //rigidBody.velocity = new Vector3(moveX, rigidBody.velocity.y, 0);
    }
}
