using UnityEngine;
using System.Collections;

public class listenerLvl2 : MonoBehaviour {

	void onTriggerEnter2D(Collider2D rigid){



		Vector3 thePosition = rigid.gameObject.transform.localPosition;
		
		thePosition.x += 5 * 0.05f;
		
		
		rigid.gameObject.transform.localPosition = thePosition;
	}
}
