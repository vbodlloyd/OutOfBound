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

			change = false;
			instance.velocity = new Vector2 (0f, 0f);
			PlayerControlerLvl2.changeState(1);
			PlayerControlerLvl2.changeState(1);
			PlayerControlerLvl2.changeGravity();
			
		}
	}
}
