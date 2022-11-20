using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Color;

public class Player : MonoBehaviour
{
    Rigidbody rb;


    private float moveH;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSmooth;

    private Vector3 speed = Vector3.zero;


    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask isFloor;

    [SerializeField] private Transform FloorController;
    [SerializeField] private Vector3 BoxDimension;
    [SerializeField] private bool onFloor;

    private bool jump = false;



    SpriteRenderer sp;
    private bool RLook = true;

    //public float speed;
    public Transform spawnPoint;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();


    }
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }



    void OnEnable()
    {
        GameController.ReSpawn += SpawnBall;

    }

    void SpawnBall()
    {
        transform.position = spawnPoint.transform.position;
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;

            // 

        }

        moveH = Input.GetAxis("Horizontal") * moveSpeed * 1000;
        FlipPlayer();
    }

    private void FixedUpdate()
    {


        //float xvelocity = moveH * speed;


        //rb.velocity = new Vector3(xvelocity, rb.velocity.y, rb.velocity.z);
       

        



        Move(moveH * Time.fixedDeltaTime, jump);
        jump = false;


    }

 




    private void Move(float move, bool Jump)
    {
        Vector3 speedTarget = new Vector3(move, rb.velocity.y, rb.velocity.z);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, speedTarget, ref speed, moveSmooth);




        if (onFloor && Jump)
        {
            onFloor = false;
            rb.AddForce(new Vector3(0f, jumpForce * 400));

        }
        /*if(move<0 && !RLook)
        {
            FlipPlayer();
        }
        else if (move>0 && RLook)
        {
            

        }*/



    }
    private void OnMouseDown()
    {


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
            //Physics2D.OverlapBox(FloorController.position, BoxDimension, 0f, isFloor);

        }
        else if(collision.gameObject.tag != "Floor") { onFloor = false; }


        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);

        }

    }
    void FlipPlayer()
    {

        /*RLook = !RLook;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;*/


        if (rb.velocity.x < -0.1f)
        {
            sp.flipX = true;
        }
        if (rb.velocity.x > 0.1f)
        {
            sp.flipX = false;
        }

    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(FloorController.position, BoxDimension);

    }
}
