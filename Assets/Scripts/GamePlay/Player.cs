using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Color;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    //Variables de movimiento
    private float moveH;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSmooth;

    private Vector3 speed = Vector3.zero;



    //Variables de salto
    [SerializeField] private float jumpForce;
    private float moveV;
    [SerializeField] private LayerMask isFloor;


    //Variables para bool tocar el suelo
    [SerializeField] private Transform FloorController;
    [SerializeField] private Vector3 BoxDimension;
    [SerializeField] private bool onFloor;

    private bool jump = false;


    //variable del animador
    private Animator animator;


    //Variable para vidas
    [SerializeField] private float Lifes = 3f;

//Variables para dar la vuelta al personaje
    SpriteRenderer sp;
    private bool RLook = true;

    //spawnpoint
    public Transform spawnPoint;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();


    }
    void Start()
    {

        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

    }



    void OnEnable()
    {
        GameController.ReSpawn += SpawnBall;

    }






    void Update()
    {//encuentra el input de la tecla horizontal y lo multiplica por velocidad modificable + aplica el metodo de girar personaje

        moveH = Input.GetAxis("Horizontal") * moveSpeed ;
        FlipPlayer();
        //encuentra el input de la tecla espacio y hacer true el bool de jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;

            

        }
        //hace el parametro del animador igual a la velocidad de movimiento (para animaciones del personaje)
        animator.SetFloat("Horizontal", Mathf.Abs(moveH));

    }

    private void FixedUpdate()
    {


    
       

        

        
        //Activa el metodo con y añade los valores de mover a la velocidad del framerate*1000, y el booleano de jump que es falso
        Move(moveH * Time.fixedDeltaTime*1000, jump);
        jump = false;


    }

 




    private void Move(float move, bool Jump)
    {//crea una velocidad que deseas y luego hace que sea el maximo de velocidad alcanzada por el personaje
        Vector3 speedTarget = new Vector3(move, rb.velocity.y, rb.velocity.z);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, speedTarget, ref speed, moveSmooth);



        //si el booleano del suelo es verdadero, y el de brincar tambien, entonces el suelo se hace falso, y se añade fuerza vertical para que brinque (y el error de la animacion aun ocurre)

        if (onFloor && Jump)
        {
            onFloor = false;
            rb.AddForce(new Vector3(0f, jumpForce * 400));



            moveV = rb.velocity.y;

            
            animator.SetFloat("Vertical", moveV);
        }
   



    }
  
    private void OnCollisionEnter(Collision collision)
    {
        //Cuando entra en collision con el objecto de caida, se activa el metodo de respawn
        if(collision.gameObject.tag == "FallDie")
        {
            SpawnBall();

        }
        if (collision.gameObject.tag == "Floor") //si hace contacto con el tag de suelo, el suelo es verdadero,y tambien el parametro
        {

            
            onFloor = true;
            animator.SetBool("OnFloor", onFloor);
           

        }
        else if(collision.gameObject.tag != "Floor") //si no lo esta tocando entonces el suelo es falso
        { 
            onFloor = false;
        }


        if (collision.gameObject.tag == "Enemy") //si toca un objeto llamado asi, se destruye
        {
            Destroy(collision.gameObject);

        }

    }
    void FlipPlayer()
    {

 //si va en direcion izquierda se activa el flip 


        if (rb.velocity.x < -0.1f)
        {
            sp.flipX = true;
        }
        if (rb.velocity.x > 0.1f)
        {
            sp.flipX = false;
        }

    }
 
    void SpawnBall()
    {//si se activa el personaje regresa al spawn point y se le resta una vida
        transform.position = spawnPoint.transform.position;
        Lifes -= 1;

        if (Lifes>= 0f)
        {
                

        }
    }
}
