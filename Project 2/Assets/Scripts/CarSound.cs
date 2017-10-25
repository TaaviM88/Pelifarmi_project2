using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarSound : MonoBehaviour {


	private AudioSource source;
	public AudioClip _carSound;


	void Start()
	{
		source = GetComponent<AudioSource> ();
	}

	void Update() 
	{
		
		if(Input.GetKeyDown(KeyCode.W))
		{   
			source.Play();
			//AudioSource.Play(44100);
		}
		if(Input.GetKeyUp(KeyCode.W))
		{
			source.Stop();
		}
			
			
	}
}
