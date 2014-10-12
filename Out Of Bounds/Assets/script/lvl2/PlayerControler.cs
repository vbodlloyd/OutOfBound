/*
Author:
Valentin 

Modification :
10/11/2014 by Valentin

Description of the Class :
Description of the character ( the hero ). Its rotation, jump, gravity and death are handle here.
*/
using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = false;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	public float moveForce = 10f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 150f;			// Amount of force added when the player jumps.
	
	public AudioClip[] jumpClips;
	public AudioClip deadClip;

	protected Animator animator;

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private Transform teleportCheck;
	
	private bool grounded = false;			// Whether or not the player is grounded.
	private bool dead = false;				// Whether or not the player is dead.
	public bool run = false;				// Whether or not the player is running.

	public static int state = 1; // 1 = gravity down, 2 = gravity left, 3 = gravity up, 4 = gravity right
	public static int previousState = 1;


	public  static void changeState(int a){
		if (a == 5)
			a = 1;
		if (a == 6)
			a = 2;
		if (a == 0)
			a = 4;
		if (a == -1)
			a = 3;
		previousState = state;
		state = a;}
	void Awake()
	{
		// Setting up references.
		animator = GetComponent<Animator>();
		groundCheck = transform.Find("groundCheck");
		teleportCheck = transform.Find("teleportCheck");
		Physics.gravity = new Vector2 (0f, -9.81f);
	}
	
	
	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
			
		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
		
		if(Physics2D.Linecast(transform.position, teleportCheck.position, 1 << LayerMask.NameToLayer("Dead")))
			dead = true;
	}
	
	
	void FixedUpdate ()
	{
		
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		if (h == 0)
			animator.SetBool("run", false );
		else
			animator.SetBool("run", true );
		changePosition (h);
		reelFlip (h);
		if (previousState != state) {
			rotate (previousState - state);
			previousState = state;}
		// If the player should jump...
		if(jump)
		{
			// Add a vertical force to the player.
			animator.SetBool("jump", true );
			jumpGravity();

			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}

		if (dead) {
			AudioSource.PlayClipAtPoint(deadClip, transform.position);

			Destroy (gameObject);
		}
	}

	void changePosition(float h){
		Vector3 thePosition = transform.localPosition;
		
		switch (state) {
		case 1 : thePosition.x += h * 0.05f;
			break;
		case 2 : thePosition.y += -1 * h * 0.05f;
			break;
		case 3 :thePosition.x +=  h * 0.05f;
			break;
		case 4 :thePosition.y += h * 0.05f;
			break;
		}
		
		transform.localPosition = thePosition;
	
	}

	public static void changeGravity()
	{
		switch(state){
		case 1 : 
			Physics2D.gravity=new Vector2(0f,-9.81f);
			break;
		case 2 : 
			Physics2D.gravity=new Vector2(-9.81f,0f);
			break;
		case 3 :
			Physics2D.gravity=new Vector2(0f,9.81f);
			break;
		case 4 :
			Physics2D.gravity=new Vector2(9.81f,0f);
			break;
		}
	}

	void reelFlip(float h){
		if (state == 1 | state == 2| state ==4) {
			// If the input is moving the player right and the player is facing left...
			if (h > 0 && !facingRight)
				// ... flip the player.
				Flip ();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (h < 0 && facingRight)
				// ... flip the player.
				Flip ();
		} else {
			// If the input is moving the player right and the player is facing left...
			if (h < 0 && !facingRight)
				// ... flip the player.
				Flip ();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (h > 0 && facingRight)
				// ... flip the player.
				Flip ();
		}
	}

	void jumpGravity(){
		switch (state) {
		case 1 : rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			break;
		case 2 : rigidbody2D.AddForce(new Vector2(jumpForce, 0f));
			break;
		case 3 :rigidbody2D.AddForce(new Vector2(0f,-1* jumpForce));
			break;
		case 4 :rigidbody2D.AddForce(new Vector2(jumpForce * -1, 0f));
			break;
		}
	
	}

	public void rotate(int n){
		if (n > 0) {
			for (int i = 0; i<n; i++) {
				transform.Rotate (new Vector3 (0f, 0f, transform.localRotation.z + 90));
			}	
		} else {
			n = n * -1;
			for (int i = 0; i<n; i++) {
				transform.Rotate (new Vector3 (0f, 0f, transform.localRotation.z - 90));
			}
		}
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
}
