using UnityEngine;
using System.Collections;

public class PlayerControlerLvl2 : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = false;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	
	
	public float moveForce = 10f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 150f;			// Amount of force added when the player jumps.
	public bool run = false;
	
	public AudioClip[] jumpClips;
	public  AudioClip teleportClip;
	public AudioClip deadClip;
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private Transform leftCheck;	
	private Transform rightCheck;	
	private Transform teleportCheck;
	protected Animator animator;
	private bool grounded = false;			// Whether or not the player is grounded.
	private bool yellowChecked = false;	
	private bool rightChecked = false;	
	private bool leftC = false;	
	private bool rightC = false;
	private bool redC = false;	
	private bool redChecked = false;
	private bool grey1C = false;	
	private bool greyChecked1 = false;
	
	private bool dead = false;
	
	
	public static int state = 1; // 1 = gravity down, 2 = gravity left, 3 = gravity up, 4 = gravity right
	public static int previousState = 1;


	public  static void changeState(int a){
		if (a == 5)
						a = 1;
		if (a == 6)
						a = 2;
		if (a == -1)
						a = 4;
		if(a==-2)
						a=3;
	


		previousState = state;
		state = a;}
	void Awake()
	{
		// Setting up references.
		animator = GetComponent<Animator>();
		groundCheck = transform.Find("groundCheck");
		leftCheck = transform.Find("leftCheck");
		rightCheck = transform.Find("rightCheck");
		teleportCheck = transform.Find("teleportCheck");
		Physics.gravity = new Vector2 (0f, -9.81f);
		//anim = GetComponent<Animator>();
	}
	
	
	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
		
		yellowChecked = Physics2D.Linecast(transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("Yellow")) |
			
			Physics2D.Linecast(transform.position, teleportCheck.position, 1 << LayerMask.NameToLayer("Yellow")); 
		
		redChecked = Physics2D.Linecast(transform.position, teleportCheck.position, 1 << LayerMask.NameToLayer("Red")) |
			
			Physics2D.Linecast(transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("Red"));
		
		greyChecked1 = Physics2D.Linecast(transform.position, teleportCheck.position, 1 << LayerMask.NameToLayer("Grey1")) |
			
			Physics2D.Linecast(transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("Grey1"));
		
		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
		
		if (yellowChecked)
			leftC = true;
		
		if (redChecked) 
			redC=true;
		
		if (greyChecked1) 
			grey1C=true;
		
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
		
		if (leftC) {
			/*Vector3 thePositionC = transform.localPosition;
			thePositionC.x = transform.position.y;
			thePositionC.y =  5.5f - transform.position.x ;
			transform.localPosition = thePositionC;*/
			transform.Rotate(new Vector3(0f,0f,transform.localRotation.z -90));
			switch (state) {
			case 1 : Physics2D.gravity=new Vector2(-9.81f,0f);state = 2;
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,0f);
				rigidbody2D.AddForce(new Vector2(0f,-150f));
				break;
			case 2 : Physics2D.gravity=new Vector2(0f,9.81f);state = 3;
				rigidbody2D.velocity = new Vector2(0f,rigidbody2D.velocity.y);
				rigidbody2D.AddForce(new Vector2(0f,-350f));
				break;
			case 3 :Physics2D.gravity=new Vector2(9.81f,0f);state = 4;
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,0f);
				rigidbody2D.AddForce(new Vector2(-150f,0f));
				break;
			case 4 :Physics2D.gravity=new Vector2(0f,-9.81f);state = 1;
				rigidbody2D.velocity = new Vector2(0f,rigidbody2D.velocity.y);
				rigidbody2D.AddForce(new Vector2(0f,350f));
				break;
			}
			
			
			leftC = false;
			
			AudioSource.PlayClipAtPoint(teleportClip, transform.position);
		}
		
		if (redC) {
			Vector3 thePositionC = transform.localPosition;
			thePositionC.x = transform.position.x - transform.position.y;
			thePositionC.y =  5.5f  ;

			transform.localPosition = thePositionC;
			transform.Rotate(new Vector3(0f,0f,transform.localRotation.z +90));
			switch (state) {
			case 1 : Physics2D.gravity=new Vector2(9.81f,0f);state = 4;
				rigidbody2D.AddForce(new Vector2(0f,150f));
				break;
			case 2 : Physics2D.gravity=new Vector2(0f,-9.81f);state = 1;
				rigidbody2D.AddForce(new Vector2(0f,350f));
				break;
			case 3 :Physics2D.gravity=new Vector2(-9.81f,0f);state = 2;
				rigidbody2D.AddForce(new Vector2(0f,-150f));
				break;
			case 4 :Physics2D.gravity=new Vector2(0f,9.81f);state = 3;
				rigidbody2D.AddForce(new Vector2(0f,-350f));
				break;
			}
			
			rigidbody2D.velocity = new Vector2(0f,rigidbody2D.velocity.y);
			redC = false;
			
			AudioSource.PlayClipAtPoint(teleportClip, transform.position);
		}
		
		if (grey1C) {
			Vector3 thePositionC = transform.localPosition;
			switch (state) {
			case 1 : Physics2D.gravity=new Vector2(0f,9.81f);
				thePositionC.y = 5.5f;
				rigidbody2D.AddForce(new Vector2(0f,-110f));
				state =3;
				rigidbody2D.velocity = new Vector2(0f,rigidbody2D.velocity.y);
				break;
			case 2 : thePositionC.x = 11.5f;
				Physics2D.gravity=new Vector2(9.81f,0f);
				rigidbody2D.AddForce(new Vector2(-110f,0f));
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,0f);
				state = 4;
				break;
			case 3 :thePositionC.y = 0.5f;
				Physics2D.gravity=new Vector2(0f,-9.81f);
				rigidbody2D.AddForce(new Vector2(0f,110f));
				state = 1;
				rigidbody2D.velocity = new Vector2(0f,rigidbody2D.velocity.y);
				break;
			case 4 :thePositionC.x = 0.5f;
				Physics2D.gravity=new Vector2(-9.81f,0f);
				rigidbody2D.AddForce(new Vector2(110f,0f));
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,0f);
				state = 2;
				break;
			}
			
			AudioSource.PlayClipAtPoint(teleportClip, transform.position);
			transform.localPosition = thePositionC;
			transform.Rotate(new Vector3(0f,0f,-180));
			grey1C = false;
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
