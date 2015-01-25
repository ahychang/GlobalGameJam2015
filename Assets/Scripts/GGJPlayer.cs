using UnityEngine;
using System.Collections;

public class GGJPlayer : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	
	
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 10f;			// Amount of force added when the player jumps.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private Transform headCheck;
	private bool grounded = false;			// Whether or not the player is grounded.
	private bool onBox = false;	
	private Animator anim;					// Reference to the player's animator component.
	private int hDir;

	AudioSource[] footsteps;
	float deathTime;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		headCheck = transform.Find("headCheck");
		anim = GetComponent<Animator>();
		footsteps = GetComponents<AudioSource>();
		hDir = 1;
		deathTime = -999f;
	}
	
	
	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
	    grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		onBox = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Box")); 
		RaycastHit2D headed = Physics2D.Raycast(headCheck.position, Vector2.up, 0.1f, 1 << LayerMask.NameToLayer("Box")); 
		
		// If the jump button is pressed and the player is grounded then the player should jump.
		if((Input.GetButtonDown("Jump")) && (grounded || onBox))
			jump = true;

		if (headed != null && headed.collider != null)
		{
			BoxScript bs = headed.collider.gameObject.GetComponent<BoxScript> ();
			if (bs != null && bs.canHurt) 
			{
				footsteps[3].Play ();
				//Application.LoadLevel("Gameover");
				deathTime = Time.time;
				if (facingRight)
				{
					transform.Rotate (0, 0, -90);
					transform.Translate(3, -1, 0);
				}
				else
				{
					transform.Rotate (0, 0, 90);
					transform.Translate(-3, -1, 0);
				}
			}
		}

		if (deathTime > 0 && Time.time > deathTime + 2)
			Application.LoadLevel("Gameover");
	}
	
	
	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		if (facingRight) hDir = 1;
		else hDir = -1;
		
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(h));

		// Play footsteps
		if (Mathf.Abs (h) > 0.01f && !(footsteps[0].isPlaying) && PlayerPrefs.GetString ("Sound") == "True" && (grounded || onBox))
		{
			footsteps[0].Play();
		}
		else if ((Mathf.Abs (h) < 0.01f && footsteps[0].isPlaying) || !(grounded || onBox))
		{
			footsteps[0].Stop();
		}
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		
		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();
		
		// If the player should jump...
		if(jump)
		{	
			footsteps[1].Play ();
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			anim.SetTrigger("Jump");
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}


		if (Input.GetKeyDown (KeyCode.Z) && DontDestroy.me.gameObject.GetComponentInChildren<SliderScript>().Full ()) 
		{
			rigidbody2D.AddForce(Vector2.right * hDir * moveForce * 20);
			anim.SetTrigger("Dash");
			footsteps[2].Play();
			DontDestroy.me.gameObject.GetComponentInChildren<SliderScript>().used = true;
			DontDestroy.me.gameObject.GetComponentInChildren<SliderScript>().setZero();
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