using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour {
	public ParticleSystem blood; //blood particles²

	void OnTriggerEnter2D(Collider2D collider) //deals with the triggers
	{
		if (collider.tag == "Player") //if this object collides with an object with a "Player" tag 
		{
			collider.GetComponent<PlayerControl2d>().Die(); //call the Die function
			blood.Play(); //will set this up later
		}
	}
}
