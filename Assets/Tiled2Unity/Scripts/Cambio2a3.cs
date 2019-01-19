using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambio2a3 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Debug.Log("Colisiona perfestamente", gameObject);
        print("Colisiona OK");
        SceneManager.LoadScene("nivel 3", LoadSceneMode.Single);

    }
}
