using UnityEngine;
using System.Collections;

public class behaveGreyOut : MonoBehaviour {

	
	public Transform sortie;
	public AudioClip teleportClip;
	
	void OnTriggerEnter2D(Collider2D rigid){
		
		
				Vector3 thePosition = rigid.gameObject.transform.localPosition;
		
				thePosition.x = sortie.position.x + 0.5f;
				thePosition.y = sortie.position.y+0.5f;
		
				rigid.gameObject.transform.localPosition = thePosition;
				PlayerControlerLvl2.changeState (PlayerControlerLvl2.state - 2);
				PlayerControlerLvl2.changeGravity ();
		
				switch (PlayerControlerLvl2.state) {
				case 1: 
						rigid.gameObject.rigidbody2D.velocity = new Vector2 (0f, rigid.gameObject.rigidbody2D.velocity.y);
						rigid.gameObject.rigidbody2D.AddForce (new Vector2 (0f, 160f));
						break;
				case 2: 
						rigid.gameObject.rigidbody2D.velocity = new Vector2 (rigid.gameObject.rigidbody2D.velocity.x, 0f);
						rigid.gameObject.rigidbody2D.AddForce (new Vector2 (160f, 0));
						break;
				case 3:
						rigid.gameObject.rigidbody2D.velocity = new Vector2 (0f, rigid.gameObject.rigidbody2D.velocity.y);
						rigid.gameObject.rigidbody2D.AddForce (new Vector2 (0f, -160f));
						break;
				case 4:
						rigid.gameObject.rigidbody2D.velocity = new Vector2 (rigid.gameObject.rigidbody2D.velocity.x, 0f);
						rigid.gameObject.rigidbody2D.AddForce (new Vector2 (-160f, 0f));
						break;
				}
		
				AudioSource.PlayClipAtPoint (teleportClip, transform.position);
		}
}
