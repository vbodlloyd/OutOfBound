/*
Author:
Valentin 

Modification :
10/11/2014 by Valentin

Description of the Class :
Behavior of the red portal. It makes the Hero goes at the exit portal position which is 90° from the enter position.
       __        exit         
		'
    	'----|   enter
*/
using UnityEngine;
using System.Collections;

public class BehaviorRedPortal : MonoBehaviour {

	public Transform exit;
	public AudioClip teleportClip;
	private float forceAdded = 160f;
	
	void OnTriggerEnter2D(Collider2D rigid){

		Vector3 thePosition = rigid.gameObject.transform.localPosition;
		int statePlayer = PlayerControler.state;
		float exitPosX = exit.position.x;
		float exitPosY = exit.position.y;
		float playerVelX = rigid.gameObject.rigidbody2D.velocity.x;
		float playerVelY = rigid.gameObject.rigidbody2D.velocity.y;

		//change state here make a rotate of 90°
		PlayerControler.changeState (statePlayer - 1);
		PlayerControler.changeGravity ();

		statePlayer = PlayerControler.state;

		switch (statePlayer) {
		case 1 : //gravity down
			thePosition.x = exitPosX+0.5f ;
			thePosition.y = exitPosY+0.5f ;
			Debug.Log(playerVelY);
			Debug.Log(playerVelX);
			rigid.gameObject.rigidbody2D.velocity = new Vector2(playerVelY,0f);
			rigid.gameObject.rigidbody2D.AddForce(new Vector2(forceAdded,0f));
			break;
		case 2 : //gravity left
			thePosition.x = exitPosX+0.5f ;
			thePosition.y = exitPosY-0.5f ;
			rigid.gameObject.rigidbody2D.velocity = new Vector2(playerVelY,0f);
			rigid.gameObject.rigidbody2D.AddForce(new Vector2(forceAdded,0f));
			break;
		case 3 : //gravity up
			thePosition.x = exitPosX+0.5f ;
			thePosition.y = exitPosY-0.5f ;
			rigid.gameObject.rigidbody2D.velocity = new Vector2(0f,-1*playerVelX);
			rigid.gameObject.rigidbody2D.AddForce(new Vector2(0f,-1*forceAdded));
			break;
		case 4 : //gravity right
			thePosition.x = exitPosX-0.5f ;
			thePosition.y = exitPosY+0.5f ;
			rigid.gameObject.rigidbody2D.velocity = new Vector2(playerVelY,0f);
			rigid.gameObject.rigidbody2D.AddForce(new Vector2(-1*forceAdded,0f));
			break;
		}
		
		rigid.gameObject.transform.localPosition = thePosition;
		
		AudioSource.PlayClipAtPoint(teleportClip, transform.position);
		
	}
}
