/*
Author:
Valentin 

Modification :
10/12/2014 by Valentin

Description of the Class :
Action to do when passing through a door ( go to next level)
*/
using UnityEngine;
using System.Collections;

public class DoorAction2 : MonoBehaviour {
	
	void OnTriggerEnter2D (Collider2D rigid )
	{
		Debug.Log("enter");
	}
	
	void OnTriggerStay2D(Collider2D rigid){
		if(Input.GetButtonDown("Jump"))
		{
			rigid.gameObject.rigidbody2D.velocity = new Vector2 (0f, 0f);
			PlayerControler.changeState(1);
			PlayerControler.changeState(1);// 2 times because of the previousState variable in PlayerControler
			PlayerControler.changeGravity();
			Application.LoadLevel ("level3");
			
		}
	}
}
