using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	//seurattu kohde
	public GameObject Target;
	//seurattavan kohteen etäisyys kamerasta
	public float Distance = 1;


	void Update ()
	{
		//Viittaus targetin transform komponenttiin
		Transform targetTransform = Target.transform; 

		//Suuntavektori
		Vector3 targetback = targetTransform.forward * -1;

		Vector3 direction = targetTransform.up + 
			targetTransform.forward * -1;

		direction = direction * Distance;

		//kameran haluttu sijainti pelimaailmassa
		Vector3 position = targetTransform.position + direction;

		transform.position = position;

		//kameran tämänhetkinen rotaatio
		Vector3 rotation = transform.eulerAngles;

		rotation.y = targetTransform.eulerAngles.y;

		transform.eulerAngles = rotation;



	}
}
