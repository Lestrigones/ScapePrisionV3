using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambio1a2 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        Debug.Log("Colisiona perfestamente", gameObject);
        print("Colisiona OK");
        SceneManager.LoadScene("nivel 2", LoadSceneMode.Single);

    }
}
