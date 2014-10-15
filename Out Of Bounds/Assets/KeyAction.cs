using UnityEngine;
using System.Collections;

public class KeyAction : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D rigid )
	{
		Debug.Log("enter");
	}
	
	void OnTriggerStay2D(Collider2D rigid){
		if(Input.GetButtonDown("Jump"))
		{

			DoorAction3.close = false;
			Destroy(gameObject);
			
		}
	}
}
