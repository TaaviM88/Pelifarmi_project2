using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_PlayerController : MonoBehaviour {
    float _speed = 10f;
    float x;
    float z;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        z = Input.GetAxis("Vertical") * Time.deltaTime * _speed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

}
