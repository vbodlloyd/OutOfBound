/*
Author:
Valentin 

Modification :
10/12/2014 by Valentin

Description of the Class :
Behavior of the blue portal. It makes the Hero goes at the exit portal position with no rotation.
       __        enter        
		'
    	'----|   exit
*/

using UnityEngine;
using System.Collections;

public class BehaviorBluePortal : MonoBehaviour {

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

		switch (statePlayer) {
		case 1 : //gravity down
			thePosition.x = exitPosX+0.5f ;
			thePosition.y = exitPosY-0.5f ;
			break;
		case 2 : //gravity left
			thePosition.x = exitPosX+0.5f ;
			thePosition.y = exitPosY-0.5f ;
			break;
		case 3 : //gravity up
			thePosition.x = exitPosX+0.5f ;
			thePosition.y = exitPosY-0.5f ;
			break;
		case 4 : //gravity right
			thePosition.x = exitPosX-0.5f ;
			thePosition.y = exitPosY-0.5f ;
			break;
		}
		
		rigid.gameObject.transform.localPosition = thePosition;
		
		AudioSource.PlayClipAtPoint(teleportClip, transform.position);
		
	}
}
