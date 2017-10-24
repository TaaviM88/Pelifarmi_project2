using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour 
{
	// tähän määritettiin viittaukset ja arvot. Koska public voidaan hienosäätää
	// myöhemmin unityssa
	Rigidbody body;
	float deadZone = 0.1f;
	public float groundedDrag = 3f;
	public float maxVelocity = 50;
	public float hoverForce = 1000;
	public float gravityForce = 1000f;
	public float hoverHeight = 1.5f;
	public GameObject[] hoverPoints;

	public float forwardAcceleration = 8000f;
	public float reverseAcceleration = 4000f;
	float thrust = 0f;

	public float turnStrength = 1000f;
	float turnValue = 0f;

	public ParticleSystem[] dustTrails = new ParticleSystem[2];

	int layerMask;

	void Start()
	{
		//tähän laitettiin referenssi auton rigidbodyyn ja luotiin layermask jota
		// käytetään myöhemmin Raycastissä. Myös rigidbodyn painopiste yhden yksikön verran
		// auton alle jolloin auto on vakaampi ja sitä on helpompi hallita ilmassakin
		body = GetComponent<Rigidbody>();
		body.centerOfMass = Vector3.down;

		layerMask = 1 << LayerMask.NameToLayer("Vehicle");
		layerMask = ~layerMask;
	}
	

	void Update()
	{
		// tässä luotiin auton eteenpäin ja taaksepäin liike "thrust"
		// Ja huomioitiin aiemmin mainittu deadzone jolla saatiin pehmennettyä ohjausta
		thrust = 0.0f;
		float acceleration = Input.GetAxis("Vertical");
		if (acceleration > deadZone)
			thrust = acceleration * forwardAcceleration;
		else if (acceleration < -deadZone)
			thrust = acceleration * reverseAcceleration;

		// Tässä luotiin aunton kääntyminen
		// Ja huomioitiin aiemmin mainittu deadzone jolla saatiin pehmennettyä ohjausta
		turnValue = 0.0f;
		float turnAxis = Input.GetAxis("Horizontal");
		if (Mathf.Abs(turnAxis) > deadZone)
			turnValue = turnAxis;
	}

	void FixedUpdate()
	{
		//  Tässä hallittiin auton Hoveri jolla määritellään myöhemmin törmäyksen aiheuttava voima
		RaycastHit hit;
		bool  grounded = false;
		for (int i = 0; i < hoverPoints.Length; i++)
		{
			var hoverPoint = hoverPoints [i];
			if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit,hoverHeight, layerMask))
			{
				body.AddForceAtPosition(Vector3.up * hoverForce* (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
				grounded = true;
			}
			else
			{
				// "tasapainon tasaaja - palauttaa auton horinzontal linjaan kun objekti ei ole grounded vaan on ilmassa
				if (transform.position.y > hoverPoint.transform.position.y)
				{
					body.AddForceAtPosition(hoverPoint.transform.up * gravityForce, hoverPoint.transform.position);
				}
				else
				{
					body.AddForceAtPosition(hoverPoint.transform.up * -gravityForce, hoverPoint.transform.position);
				}
			}
		
		}
		// Tässä hallittiin auton käytöstä kun auto on ilmassa/maassa JA
		// lisättiin pölyvana(dusttrail) efekti pyöriin ja poistettiin se
		// kun auto on ilmassa
		var emissionRate = 0;
		if(grounded)
		{
			body.drag = groundedDrag;
			emissionRate = 10;
		}
		else
		{
			body.drag = 0.1f;
			thrust /= 100f;
			turnValue /= 100f;
		}

		for(int i = 0; i<dustTrails.Length; i++)
		{
			var emission = dustTrails[i].emission;
			emission.rate = new ParticleSystem.MinMaxCurve(emissionRate);
		}

		// Tässä hallitaan auton eteen ja taakse suuntautuvia voimia
		if (Mathf.Abs(thrust) > 0)
			body.AddForce(transform.forward * thrust);

		// Tässä hallittiin auton käännöksen voimia
		if (turnValue > 0)
		{
			body.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
		} else if (turnValue < 0)
		{
			body.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
		}

		// Tässä hallittiin voimien kiihtyvyyttä (velocity)
		if(body.velocity.sqrMagnitude > (body.velocity.normalized * maxVelocity).sqrMagnitude)
		{
			body.velocity = body.velocity.normalized * maxVelocity;
		}
	}
}
