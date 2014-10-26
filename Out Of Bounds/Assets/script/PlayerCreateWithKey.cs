/*
Author:
Valentin 

Modification :
10/26/2014 by Valentin

Description of the Class :
Instantiate an instance of the Hero and an instance of a key
*/
using UnityEngine;
using System.Collections;

public class PlayerCreate : MonoBehaviour {
	
	public Rigidbody2D hero;
	public Transform origin;
  	public RigidBody2D key;
  	public Transform originKey;
	public Rigidbody2D instance;
  	public RigidBody2D instanceKey;
	private bool change = false;
	
	// Use this for initialization
	void Start () {
		instance = Instantiate (hero, origin.position, origin.rotation) as Rigidbody2D;
      	instanceKey = instantiate(key, originKey.position, originKey.rotation) as RigidBody2D;
	}

	// Update is called once per frame
	void Update () {
		if (instance == null) {
			Start ();
			change = true;
		}
	}

	void FixedUpdate(){
		if (change) {
			change = false;
			instance.velocity = new Vector2 (0f, 0f);
			PlayerControler.changeState(1);
			PlayerControler.changeState(1);// 2 times because of the previousState variable in PlayerControler
			PlayerControler.changeGravity();
			
		}
	}
}