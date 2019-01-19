using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policia1 : MonoBehaviour {

    public float maxSpeed = 1f;
    public float speed = 1f;
    private Rigidbody2D rb2d;
    Vector2 mov;


    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //rb2d.velocity = Vector2.left * velocity;
        //rb2d.velocity = Vector2.right * velocity;


    }
    private void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * speed);
        //limitamos la velocidad
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        //cambiara de movimiento solo
        if (rb2d.velocity.x > -0.05f && rb2d.velocity.x < 0.05f)
        {
            speed = -speed;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

        }
        if (speed < 0)
        {
            transform.localScale = new Vector3(2f, 2f, 2f);
        }
        else if (speed > 0)
        {
            transform.localScale = new Vector3(-2f, 2f, 2f);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player1")
        {
            Debug.Log("player detectado");
            col.SendMessage("EnemyKnockBack", transform.position.x);

        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
