using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEnemy : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;     
	public int attackDamage = 10;

	GameObject player;                       
	PlayerHealth playerHealth;                  
	bool playerInRange;
	float timer;

	// Use this for initialization
	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}
	
	//määritetään pelaajan havaitseminen
	void OnCollisionEnter (Collision other) 
	{
		if (other.gameObject == player) 
		{
			playerInRange = true;
		}
	}

	void OnCollisionExit (Collision other)
	{
		if (other.gameObject == player) 
		{
			playerInRange = false;
		}
	}

	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(playerInRange)
		{
			// ... attack.
			Attack ();
		}
	
	}

	void Attack ()
	{
		// Reset the timer.
		timer = 0f;

		// If the player has health to lose...
		if(playerHealth.currentHealth > 0)
		{
			// ... damage the player.
			playerHealth.TakeDamage (attackDamage);
		}
	}

}
