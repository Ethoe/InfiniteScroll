using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject groundCheck;
    public GameObject leftCheck;
    public GameObject rightCheck;
    public float moveSpeed;
    public float jumpSpeed;
    public float acceleration;

    Rigidbody rigidBody;
    float moveX;
    int moveDir;
    bool jumped;
    bool grounded;
    bool wallJumped;
    bool onWall;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    void Update()
    {
        moveDir = (int)Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumped = true;
        }
        if (Input.GetButtonDown("Jump") && moveDir != 0 && onWall && !grounded)
        {
            wallJumped = true;
        }
    }

    void FixedUpdate()
    {
        if (moveDir != 0 || !grounded)
            moveX = Mathf.MoveTowards(moveX, moveDir * moveSpeed, Time.deltaTime * acceleration);
        else
            moveX = Mathf.MoveTowards(moveX, moveDir * moveSpeed, Time.deltaTime * acceleration * 2f);

        rigidBody.velocity = new Vector3(moveX, rigidBody.velocity.y, 0);

        grounded = Physics.CheckSphere(groundCheck.transform.position, .2f, LayerMask.GetMask("Terrain"));
        onWall = Physics.CheckSphere(leftCheck.transform.position, .2f, LayerMask.GetMask("Terrain")) || Physics.CheckSphere(rightCheck.transform.position, .2f, LayerMask.GetMask("Terrain"));

        if (rigidBody.velocity.y < .75f * jumpSpeed || !Input.GetButton("Jump"))
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * 5f;
        }

        if (jumped)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed, 0);
            jumped = false;
        }

        if (wallJumped)
        {
            rigidBody.velocity = new Vector3(-1 * rigidBody.velocity.x, jumpSpeed, 0);
            moveX *= -1;
            wallJumped = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(leftCheck.transform.position, .2f);
        Gizmos.DrawSphere(rightCheck.transform.position, .2f);
    }
}
