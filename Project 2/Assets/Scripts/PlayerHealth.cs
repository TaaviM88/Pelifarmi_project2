using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public AudioClip explosion;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	AudioSource playerAudio;
	Controller _controller;

	bool isDead;
	bool damaged; 

	// Use this for initialization
	void Awake () 
	{
		_controller = GetComponent<Controller> ();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the player has just been damaged...
		if (damaged) {
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else {
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}

		public void TakeDamage (int amount)
		{
			// Set the damaged flag so the screen will flash.
			damaged = true;

			// Reduce the current health by the damage amount.
			currentHealth -= amount;

			// Set the health bar's value to the current health.
			healthSlider.value = currentHealth;

			// Play the hurt sound effect.
			//playerAudio.Play ();

			// If the player has lost all it's health and the death flag hasn't been set yet...
			if(currentHealth <= 0 && !isDead)
			{
				// ... it should die.
				Death ();
			}
		}

		void Death ()
		{
			// Set the death flag so this function won't be called again.
			isDead = true;

			// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
			//playerAudio.clip = explosion;
			//playerAudio.Play ();

			// Turn off the movement and shooting scripts.
			_controller.enabled = false;

		}       
}
