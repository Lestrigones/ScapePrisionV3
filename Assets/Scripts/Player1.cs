using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class Player1 : MonoBehaviour {

    Rigidbody2D rb2d;
    Vector2 mov;
    private SpriteRenderer spr;
    public bool PlayerCollider;
    GameObject Healthbar;
    public bool movement = true;
    bool movePrevent;

    int contadorpuntos;
    public Text Score;

    //public Text winstxt;
    //public GameObject WinsText;

    private Vector2 touchOrigin = -Vector2.one;


    public GameObject gamewins;
    public Text gameWinsText;


    public float speed = 4f;

    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        gamewins.SetActive(false);

        Healthbar = GameObject.Find("Healthbar");//divino!! funciona como dios manda!! :)))

    }

   
    
       
    

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.transform.tag == "clau")
        {
            Destroy(obj.transform.gameObject);
            contadorpuntos = contadorpuntos + 1;
            Score.text = "Score:" + contadorpuntos;

        }
        if (obj.transform.name == "navaja")
        {
            Destroy(obj.transform.gameObject);
            contadorpuntos = contadorpuntos + 1;
            Score.text = "Score:" + contadorpuntos;

        }
        if (obj.transform.tag == "cuerda")
        {
            Destroy(obj.transform.gameObject);
            contadorpuntos = contadorpuntos + 1;
            Score.text = "Score:" + contadorpuntos;

        }
        if (obj.transform.tag == "Exit")
        {

            Destroy(obj.transform.gameObject);
            gamewins.SetActive(true);
            Over.show();



        }


    }

    // Use this for initialization
    
	
	// Update is called once per frame
	void Update () {

        Movements();

        PreventMovement();

        
        
            
        


        /*Vector3 mov = new Vector3(
            Input.GetAxisRaw("Horizontal"), //cuidado con las mayusculas y minisculas
            Input.GetAxisRaw("Vertical"),
            0
            );//movientos de teclado
        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.position + mov,
            speed * Time.deltaTime
            );*/



    }
    public void EnemyKnockBack()//float enemyPosX
    {
        Healthbar.SendMessageUpwards("TakeDamage", 33);
        



        //Healthbar.SendMessage("TakeDamage", 33);
        Debug.Log("colisiona ok");

        //devuelve el lado del golpe
        //float side = Mathf.Sign(enemyPosX - transform.position.x);
        //rb2d.AddForce(Vector2.left * side, ForceMode2D.Impulse);

        movement = false;
        Invoke("EnableMovement", 0.7f);

        Color color = new Color(255 / 255f, 106 / 255f, 0 / 255f);//cambiaamos de color del player cuando choca con el enemigo asi sabremos que le ha dao
        spr.color = color;
    }
    void Movements()//movemos la jugador
    {

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        //detectamos el movimiento 2d
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );

        //GESTOS TACTILES
#else
       
        if(Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];
            if(myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
                
            } else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    horizontal = x > 0 ? 1 : -1;
                else
                    vertical = y > 0 ? 1 : -1;
                 
            }
        }

#endif



    }
    void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;//su estado normal
    }
    private void FixedUpdate()
    {

        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);//velosidadddd del player la podemos modificar en el inspector
        float h = Input.GetAxis("Horizontal");
        if (!movement) h = 0;



    }
    void PreventMovement()//es una funcion que hace que cuando ataque se quede quieto
    {
        if (movePrevent)
        {
            mov = Vector2.zero;
        }
    }
   




}

