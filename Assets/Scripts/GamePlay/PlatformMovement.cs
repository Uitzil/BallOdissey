using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    [SerializeField] private Transform FloorControl;
    [SerializeField] private float Distancia;
    public float speed = 1;
    //[SerializeField] bool turn;

    public bool moveRight;
    private Rigidbody rb; 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    private void FixedUpdate()
    {
        //RaycastHit infoGround = Physics.Raycast(FloorControl.position, Vector3.down, Distancia);

        rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

        if (Physics.Raycast(FloorControl.position, Vector3.down, Distancia))
        {
           // turn = false;
            Turn();
        }

        
        

    }
    private void Turn()
    {
        moveRight = !moveRight;
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        speed *= -1;

    }

     private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(FloorControl.transform.position, FloorControl.transform.position + Vector3.down * Distancia);
    }
}
