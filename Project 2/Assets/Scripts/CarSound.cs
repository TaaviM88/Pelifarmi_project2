using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarSound : MonoBehaviour 
{

	AudioSource engine;
	AudioSource impact;
	AudioSource impact2;



	void Start()
	{
		AudioSource[] audios = GetComponents<AudioSource>();
		engine = audios[0];
		impact = audios[1];
	}

	void Update() 
	{
		//tehty useampi sama komento koska oon vitun tyhmä ja väsyttää
		if(Input.GetKeyDown(KeyCode.W))
		{   
			engine.Play();
			//AudioSource.Play(44100);
		}
		if(Input.GetKeyUp(KeyCode.W))
		{
			engine.Stop();
		}

		if(Input.GetKeyDown(KeyCode.S))
		{   
			engine.Play();
			//AudioSource.Play(44100);
		}
		if(Input.GetKeyUp(KeyCode.S))
		{
			engine.Stop();
		}

			
	}

	void OnCollisionEnter (Collision Collision)
	{
		if(GetComponent<Collider>().gameObject)
		{
			impact.Play();
		}
			
	}

}
