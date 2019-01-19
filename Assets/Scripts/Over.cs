using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Over : MonoBehaviour {


    public GameObject GameOverText;
    public static GameObject GameOverStatic;

    
    
    // Use this for initialization


    void Start () {

        Over.GameOverStatic = GameOverText;
        Over.GameOverStatic.gameObject.SetActive(false);
    }
	
    public static void show()
    {
        Over.GameOverStatic.gameObject.SetActive(true);

    }

	// Update is called once per frame
	void Update () {
		
	}
}
