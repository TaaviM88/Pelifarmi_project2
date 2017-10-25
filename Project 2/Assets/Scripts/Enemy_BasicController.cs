using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BasicController : MonoBehaviour {
    public float speed = 5f;
    public float directionChangeInterval = 1f;
    public float maxheadingChange = 30f;
    bool _dying;
    float heading;
    Vector3 targetRotation;
    Rigidbody _rb;
    //CharacterController controller;
	// Use this for initialization
	void Awake () {
        //Invoke("DestroyEnemy", 20f);
        //controller = GetComponent<CharacterController>();
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(Newheading());
        _dying = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        transform.Translate(forward*speed*Time.deltaTime);
        if(_dying == true)
        {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }
        //controller.SimpleMove(forward * speed);
        //Debug.Log(targetRotation);
	}

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    IEnumerator Newheading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void NewHeadingRoutine()
    {
       
        var floor = Mathf.Clamp(heading - maxheadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxheadingChange,0,360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Building") || col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Perkele kun oon kännissä");
            //NewHeadingRoutine();
            targetRotation -= new Vector3(0, 60, 0);
        }
        if(col.gameObject.CompareTag ("Player"))
        {
            ReleaseFreeze();
            Dying();
        }
    }

    void ReleaseFreeze()
    {
        _rb.constraints = RigidbodyConstraints.None;
    }

    void Dying()
    {
        _dying = true;
        Invoke("Death",10f);
    }

    void Death()
    {
        Destroy(gameObject);
    }
    /*void OnCollisionStay(Collision ColInfo)
    {
        foreach (ContactPoint contact in ColInfo.contacts)
        {
            Debug.Log("Perkele kun oon kännissä");
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }
        /*if (ColInfo.collider.CompareTag("Building"))
        {
            Debug.Log("Perkele kun oon kännissä");
        }
    }*/

}
