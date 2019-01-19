using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanza : MonoBehaviour {

    [Tooltip("Velocidad de movimiento en unidades del mundo")]//son los cuadrados del mapa
    public float speed;

    GameObject player;
    Rigidbody2D rb2d;
    Vector3 target, dir; // almacena el objetivo y si direccion


    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player1");
        rb2d = GetComponent<Rigidbody2D>();

        //recuperamos posicion de ataque del jugador
        if(player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;//el vector normalizado de la direccion
        }
		
	}
    void FixedUpdate()
    {
        // Si hay un objetivo movemos la lanza hacia su posición
        if (target != Vector3.zero)
        {
            rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player1")
        {

            col.SendMessage("EnemyKnockBack", transform.position.x);//quita vida al player 33% 

        }
        // Si chocamos contra el jugador o un ataque la borramos
        if (col.transform.tag == "Player1" || col.transform.tag == "Attack")
        {
            Destroy(gameObject);//parece ser que no lo borra ;(((
        }
    }
   

    void OnBecameInvisible()
    {
        // Si se sale de la pantalla borramos la roca
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}


}
