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
    [SerializeField] private float timeTodie = 1.7f;
    private float moveV;
    [SerializeField] private LayerMask isFloor;


    //Variables para bool tocar el suelo
    [SerializeField] private Transform FloorController;
    [SerializeField] private Vector3 BoxDimension;
     private bool onFloor=false;
    private bool withBox=false;

    private bool jump = false;


    //variable del animador
    private Animator animator;


    //Variable para vidas
    public float Lifes = 6f;
    private bool onDie;

//Variables para dar la vuelta al personaje
    SpriteRenderer sp;
  

    //spawnpoint
    public Transform spawnPoint;


    //EVENTOS
    public delegate void dieAction();
    public static event dieAction DiePlayer;

    public delegate void overAction();
    public static event overAction GameOver;

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
    { //suscripcion del metodo de killplayer a el evento de respawn
      GameController.ReSpawn += KillPlayer;
        

    }






    void Update()
    {//encuentra el input de la tecla horizontal y lo multiplica por velocidad modificable + aplica el metodo de girar personaje
        if (onDie) 
            return;

        moveH = Input.GetAxis("Horizontal") * moveSpeed ;
        FlipPlayer();
        //encuentra el input de la tecla espacio y hacer true el bool de jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;

            

        }
        //hace el parametro del animador igual a la velocidad de movimiento (para animaciones del personaje)
        animator.SetFloat("Horizontal", Mathf.Abs(moveH));
        moveV = rb.velocity.y;



        animator.SetFloat("Vertical", moveV);

        animator.SetBool("OnFloor", onFloor);

        animator.SetBool("WithBox", withBox);


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

        if (onFloor && Jump || withBox && Jump)
        {
            
            rb.AddForce(new Vector3(0f, jumpForce * 400));
           


        }
   



    }
  
    
    private void OnCollisionEnter(Collision collision)
    {
        //Cuando entra en collision con el objecto de caida, se activa el metodo de killplayer
        if(collision.gameObject.tag == "FallDie")
        {
            KillPlayer();

        }
        if (collision.gameObject.tag == "Floor") //si hace contacto con el tag de suelo, el suelo es verdadero,y tambien el parametro
        {

            
            onFloor = true;
            
           
            
        }
        
      
        if (collision.gameObject.tag == "Box") // si entra en contacto con un objeto de tag box entonces el bool de withbox es cierto y tambien el paramentro
        {
            withBox = true;

        }

            if (collision.gameObject.tag == "Paletas") //si toca un objeto con tag paleta, se destruyen las paletas y se suma una vida
        
        {
            Destroy(collision.gameObject);
            Lifes += 1;
        }

    }
    private void OnCollisionExit(Collision collision) //cuando deja e tocar alguno de esos objetos su bool es falso
    {
        if (collision.gameObject.tag == "Box")
        {
            withBox = false;

        }
        if (collision.gameObject.tag == "Floor") 
        {


            onFloor = false;



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
    private void KillPlayer() { //cuando se activa el bool de morir es positivo y se informa el parametro, si tienes mas de 0 vidas se activa el respawn y si no gameover

        if (onDie) return;

        onDie = true;
        animator.SetBool("OnDie", onDie);
        if (Lifes > 0f)
        {
            StartCoroutine(SpawnBall());
            Lifes -= 1;
            if (DiePlayer != null)
                    DiePlayer();
            }
        if (Lifes ==0) 
        {
            if (GameOver != null)
                GameOver();
        }
        
    }
 
    IEnumerator SpawnBall()
    {//si se activa se toma unos segundos para el respawn, luego el personaje regresa al spawn point y se le resta una vida, Y el on die se vuelve falso 
        yield return new WaitForSeconds(timeTodie);
        transform.position = spawnPoint.transform.position;
      
        onDie = false;
        animator.SetBool("OnDie", onDie);
    }
}
