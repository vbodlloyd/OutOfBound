using UnityEngine;
using System.Collections;

public class listener : MonoBehaviour {


	void OnTriggerEnter2D (Collider2D rigid )
	{
		Debug.Log("enter");


	}

	void OnTriggerStay2D(Collider2D rigid){
		if(Input.GetButtonDown("Jump"))
		{
			Physics2D.gravity=new Vector2(0f,-9.81f);
			Application.LoadLevel ("level2");
		}
	}
}
