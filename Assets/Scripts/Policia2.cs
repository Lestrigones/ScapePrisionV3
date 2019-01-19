using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Policia2 : MonoBehaviour
{
    //cuidado con la layers de cada cosa!!!! activaremos y desactivaremos capas entre ellas 

        // para que persigan al preso las capas en el enemigo son: player1 y default

    //variables para la vision del enemigo crear una esfera 
    public float visionRadius;
    public float attackRadius;
    public float speed;

    // Variables relacionadas con el ataque
    [Tooltip("Prefab de la lanza que se disparará")]
    public GameObject LanzaPrefab;
    [Tooltip("Velocidad de ataque (segundos entre ataques)")]
    public float attackSpeed = 2f; //segundos
    bool attacking; //cuando no ataque sera falso es privada


    ///--- Variables relacionadas con la vida
    [Tooltip("Puntos de vida")]
    public int maxHp = 3;
    [Tooltip("Vida actual")]
    public int hp;

    GameObject player;//variable para guardar el jugador
    Vector3 initialPosition, target;//posicion inicial

    Rigidbody2D rb2d;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        //recuperamos al jugador
        player = GameObject.FindGameObjectWithTag("Player1");

        initialPosition = transform.position;//guardamos la pos ini

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        ///--- Iniciamos la vida
        hp = maxHp;


    }

    // Update is called once per frame
    private void Update()
    {
         target = initialPosition;//objetivo player

         

        // Comprobamos un Raycast del enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,//miramos hacia el jugador
            visionRadius * 30f,
            1 << LayerMask.NameToLayer("Default")
        // Poner el propio Enemy en una layer distinta a Default para evitar el raycast
        // También poner al objeto Attack y al Prefab Slash una Layer Attack 
        // Sino los detectará como entorno y se mueve atrás al hacer ataques
        );

        // Aquí podemos debugear el Raycast mirando al jugador 
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player1")//si es visible atacamos
            {
                target = player.transform.position;
            }
        }
        // Calculamos la distancia y dirección actual hasta el target
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;
        //vector con la direccion que se mueve


        if (target != initialPosition && distance < attackRadius)
        {
            // Aquí le atacaríamos, la animación
           anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.Play("Policia_walk", -1, 0);  // Congela la animación de andar
          
            ///-- Empezamos a atacar (importante una Layer en ataque para evitar Raycast)
            if (!attacking) StartCoroutine(Attack(attackSpeed));//super importanteeeeeeeee 
            //la corrutina no se puede llamar mas de dos segundos
        }
        // En caso contrario nos movemos hacia él
        else
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

            // Al movernos establecemos la animación de movimiento
            anim.speed = 1;
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.SetBool("walking", true);
        }

        // Una última comprobación para evitar bugs forzando la posición inicial
        //esta parte la habiamos comentado porque daba problemas
       //if (target == initialPosition && distance< 0.05f){
         //transform.position = initialPosition; 
            // Y cambiamos la animación de nuevo a Idle
          // anim.SetBool("walking", false);
       
        // Y un debug optativo con una línea hasta el target
       // Debug.DrawLine(transform.position, target, Color.green);

        //esta parte esta dando muchos problemas y no se porque...por eso esta comentado

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player1")
        {

            col.SendMessage("EnemyKnockBack", transform.position.x);

        }
    }
    void OnDrawGizmosSelected()
        {

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, visionRadius);
            Gizmos.DrawWireSphere(transform.position, attackRadius);

        }

        //corrutina cada cierto tiempo
        IEnumerator Attack(float seconds) // ataque cada 2 segundos super importante
        {

            attacking = true;  // Activamos la bandera
                           // Si tenemos objetivo y el prefab es correcto creamos la lanza que no funciona :DDDDD solo si la coloco manual 
            target = initialPosition;
            if (target != initialPosition && LanzaPrefab != null)
            {
            Instantiate(LanzaPrefab, transform.position, transform.rotation);//es asi y punto
            // Esperamos los segundos de turno antes de hacer otro ataque
            yield return new WaitForSeconds(seconds);
            }
            attacking = false; // Desactivamos la bandera es decir el ataque, una vez que se acabe el ciclo
    }
    ///--- Gestión del ataque, restamos una vida
    public void Attacked(){//atacado por preso

        if (--hp <= 0) Destroy(gameObject);//restamos directamente en la instruccion para ahorrar codigo
    }
    ///---  Dibujamos las vidas del enemigo en una barra 
    void OnGUI()
    {
        // Guardamos la posición del enemigo en el mundo respecto a la cámara
       Vector2 pos = transform.position;

        // Dibujamos el cuadrado debajo del enemigo con el texto

        // GUI.Box(new Rect(0, 0, Screen.width, Screen.height), content);
        GUI.Box(
            new Rect(
                pos.x - 20,                   // posición x de la barra
                Screen.height - pos.y + 60,   // posición y de la barra
                40,                           // anchura de la barra    
                24                            // altura de la barra  
            ), hp + "/" + maxHp               // texto de la barra la info de la vidas lo que me quita / y el max en este caso son 3 vidas max
        );
    }

}
