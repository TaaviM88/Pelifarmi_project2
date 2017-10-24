using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BasicController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroyEnemy", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
