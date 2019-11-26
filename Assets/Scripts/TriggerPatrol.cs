using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPatrol : MonoBehaviour {

	// Use this for initialization
	NPC musuh;
	SphereCollider myCollider;
	public float InRadius;

	void Start () {
		musuh = GetComponentInParent<NPC>();
		myCollider = GetComponent<SphereCollider>();
		myCollider.radius = InRadius;
		myCollider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider coll){
		if(coll.gameObject.tag == "Pemain")
		{
			print("pemain didalam radius");
			musuh.target = coll.gameObject;
		}
	}
	void OnTriggerExit(Collider coll){
		if(coll.gameObject.tag == "Pemain")
		{
			print("pemain diluar radius");
			musuh.target = null;
		}
	}
}
