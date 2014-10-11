using UnityEngine;
using System.Collections;

public class PlayerCreate : MonoBehaviour {


	public Rigidbody2D hero;
	public Transform origin;
	public int abracadrabra = 0;
	public Rigidbody2D instance;
	private bool change = false;
	
	// Use this for initialization
	void Start () {

		instance = Instantiate (hero, origin.position, origin.rotation) as Rigidbody2D;
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
			Physics2D.gravity = new Vector2 (0f,-9.81f);
			change = false;
			instance.velocity = new Vector2 (0f, 0f);
			
		}
	}
}
