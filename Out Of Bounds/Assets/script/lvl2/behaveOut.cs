using UnityEngine;
using System.Collections;

public class behaveOut : MonoBehaviour {

	public Transform sortie;
	public AudioClip teleportClip;
	
	void OnTriggerEnter2D(Collider2D rigid){
		
		
		Vector3 thePosition = rigid.gameObject.transform.localPosition;
		

		PlayerControlerLvl2.changeState (PlayerControlerLvl2.state - 1);
		PlayerControlerLvl2.changeGravity ();

		switch (PlayerControlerLvl2.state) {
		case 1 : 
			thePosition.x = sortie.position.x+0.5f ;
			thePosition.y = sortie.position.y+0.5f ;
			rigid.gameObject.rigidbody2D.velocity = new Vector2(rigid.gameObject.rigidbody2D.velocity.x,0f);
			rigid.gameObject.rigidbody2D.AddForce(new Vector2(160f,0f));
			break;
		case 2 : 
			thePosition.x = sortie.position.x+0.5f ;
			thePosition.y = sortie.position.y-0.5f ;
			rigid.gameObject.rigidbody2D.velocity = new Vector2(0f,rigid.gameObject.rigidbody2D.velocity.y);
			rigid.gameObject.rigidbody2D.AddForce(new Vector2(0f,-160f));
			break;
		case 3 :
			thePosition.x = sortie.position.x+0.5f ;
			thePosition.y = sortie.position.y-0.5f ;
			rigid.gameObject.rigidbody2D.velocity = new Vector2(rigid.gameObject.rigidbody2D.velocity.x,0f);
			rigid.gameObject.rigidbody2D.AddForce(new Vector2(-160f,0f));
			break;
		case 4 :
			thePosition.x = sortie.position.x-0.5f ;
			thePosition.y = sortie.position.y+0.5f ;
			rigid.gameObject.rigidbody2D.velocity = new Vector2(0f,rigid.gameObject.rigidbody2D.velocity.y);
			rigid.gameObject.rigidbody2D.AddForce(new Vector2(0f,160f));
			break;
		}
		rigid.gameObject.transform.localPosition = thePosition;
		AudioSource.PlayClipAtPoint(teleportClip, transform.position);
		
	}
}
