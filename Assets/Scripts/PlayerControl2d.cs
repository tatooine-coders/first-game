using UnityEngine;
using System.Collections;

public class PlayerControl2d : MonoBehaviour 
{
	public int moveSpeed; //the speed at which the player moves
	public int jumpForce; //the force added when the player jumps

	public Transform groundPoint; //the position at which we check the grounded state of the player
	public float radius; //the radius from the position above
	public LayerMask groundMask; //this is used to insure that what the player is touching is considered a ground

	public GameObject playerSprite; //the children of the player, the one containing all the animations
	public Animator playerAnimator; //the animator of playerSprite


	public bool isGrounded; //returns true/false if the player is grounded/not grounded (meaning touching the ground)
	private Rigidbody2D rb2d; //the rigidbody component of the player

	//we use the start function to initialize our variables
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D>(); /*GetComponent tries to find the <component> attached 
		to the object this script is attached to and assigns it to the variable rb2d*/
		playerAnimator = playerSprite.GetComponent<Animator> (); //we initialize the animator of playerSprite
	}
	
	// Update is called once per frame
	void Update () 
	{
		isGrounded = Physics2D.OverlapCircle (groundPoint.position, radius, groundMask); /*this function returns true if this object 
		touches another object belonging to the "groundMask" layer, from "groundPoint.position" and at a radius of "radius", returns false
		otherwise*/

		if(Mathf.Abs(Input.GetAxisRaw ("Horizontal")) == 1) //returns true if the player is moving left or right
		{
			playerAnimator.SetBool ("isMoving", true); //we set the playerAnimator bool "isMoving" to true, in order to play the move animation
			Vector2 moveDirection = new Vector2 (Input.GetAxisRaw ("Horizontal") * moveSpeed, rb2d.velocity.y); /*Every frame, 
			we create a new Vector2 that takes the horizontal input (x) and the y velocity of the rigidbody*/
			rb2d.velocity = moveDirection; //we assing the vector above to the velocity of the object
			transform.localScale = new Vector3 (Input.GetAxisRaw ("Horizontal"), 1, 1); //we flip the x transform depending on the direction
		}

		else
		{
			playerAnimator.SetBool ("isMoving", false); //we stop the move animation and play the idle one
			Vector2 moveDirection = new Vector2 (0, rb2d.velocity.y); //we nullify the horizontal velocity
			rb2d.velocity = moveDirection; //same as above
		}

		if (Input.GetButtonDown ("Jump") && isGrounded) //if we hit the jump button, and the player is touching the ground
		{
			rb2d.AddForce (new Vector2 (0, jumpForce)); //"jump" by adding a vertical force of "jumpForce" to the player
		}
	}

	public void Die() //called when we die
	{
		Application.LoadLevel (0); //restarts the level
	}
}
