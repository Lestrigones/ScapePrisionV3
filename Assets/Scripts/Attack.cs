using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    //Este ataque es para matar al madero

    void OnTriggerEnter2D(Collider2D col)
    {
        ///--- Restamos uno de vida si es un enemigo
        if (col.tag == "Policia2") col.SendMessage("Attacked");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
