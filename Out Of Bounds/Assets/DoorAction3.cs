using UnityEngine;
using System.Collections;

public class DoorAction3 : MonoBehaviour {

	public  static bool  close = true;

	void OnTriggerEnter2D (Collider2D rigid )
	{
		Debug.Log("enter");
	}
	
	void OnTriggerStay2D(Collider2D rigid){
		if(Input.GetButtonDown("Jump"))
		{
			if(close)
				return;
			rigid.gameObject.rigidbody2D.velocity = new Vector2 (0f, 0f);
			PlayerControler.changeState(1);
			PlayerControler.changeState(1);// 2 times because of the previousState variable in PlayerControler
			PlayerControler.changeGravity();
			Application.LoadLevel ("level3");
			
		}
	}
}
