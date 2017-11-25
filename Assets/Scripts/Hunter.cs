using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {

	[SerializeField] private Transform player; // player's location

	//[SerializeField] private Rigidbody2D playerBody; // player's Rigidbody

	[SerializeField] private NodeGrid grid; // grid of nodes

	[SerializeField] private Rigidbody2D body; // rigid body for movement

	[SerializeField] private float sightRange = 15f; // how far it can see the player

	[SerializeField] private float moveSpeed = 5f; // patrol movement speed, alert = 2x this

	[SerializeField] private float moveRadus = 0.25f; // distance to the destination target will stop at

	[SerializeField] private float abilityRadius = 1.5f; // rangeat which the Hunter uses it's ability
	
	[SerializeField] private int reactionTime = 10; // number of A* calls to ignore

	private NodeList moveList; // list of nodes to move to

	private Node destinationNode;
	private Vector3 destination, vector;

	private bool alert, sight;
	private AStar aStar;

	private RaycastHit2D hit;

	private int starCounter; // countdown to get to next A* calculation

	// Use this for initialization
	void Start () {	
		destination = transform.position;
		moveList = new NodeList();//new NodeWrapper(grid.getNode((int)destination.x, (int)destination.y)));
		alert = sight = false;
		starCounter = reactionTime;
		aStar = new AStar(grid);
	}
	
	
	void FixedUpdate() {
		if(moveList == null) {
			moveList = new NodeList();
			Debug.Log("Null MoveList replaced with blank");
		}

		//Debug.Log("Destination is: " + destination);
		vector = destination - transform.position;
		// if position is too far from 'destination' move
		if ((vector).magnitude > moveRadus) {
			// move, capping movement speed
			if(vector.magnitude > moveSpeed || vector.magnitude < 3f) { // TODO: setting move speeds
				vector.Normalize();
				vector *= moveSpeed;
			}
			body.velocity = vector;
		}
		// else get next position from NodeList
		else {
			destinationNode = moveList.pop();
			if (destinationNode != null) {
				destination = destinationNode.getPosition();
			}
			else {
				// determine node to go to
			}
		}
		
		// LOS check
		if(lineOfSight())
		{
			Debug.Log("Line of Sight Extablished");
			sight = true;
			// if within radius, use special, don't update moves
			if((player.position - transform.position).magnitude < abilityRadius) {
				special();
			}
			// A* to player
			else if(starCounter < 1) {
				/*
				moveList = aStar.findPath(grid.getNode(0,0).convertX(transform.position.x),
										  grid.getNode(0,0).convertY(transform.position.y),
										  grid.getNode(0,0).convertX(player.position.x),
										  grid.getNode(0,0).convertY(player.position.y)); */
				//moveList.clear();
				moveList = aStar.findPath((int)transform.position.x, (int)transform.position.y,
											(int)player.position.x, (int)player.position.y);
				//Debug.Log("Created New Path with A* from " + transform.position + " to " + player.position);
				starCounter = reactionTime;
			}
			else
				starCounter--;
		}
		else{
			// LOS just lost
			if(sight) {
				Debug.Log("Line of Sight Lost");
				// stop moving/empty move list
				moveList.clear();
				sight = false;
				// A* to location player is heading towards (if able)
				/*moveList = aStar.findPath((int)transform.position.x, (int)transform.position.y,
											(int)(player.position.x + playerBody.velocity.x), 
											(int)(player.position.y + playerBody.velocity.y));*/
				// Alternate: A* to the player's current location (if able)
				moveList = aStar.findPath((int)transform.position.x, (int)transform.position.y,
											(int)player.position.x, (int)player.position.y);
				starCounter = reactionTime;
			}
		}
	}

	// determine if has player in line of sight, overwritten in children
	private bool lineOfSight() {
		
		Vector3 direction = (player.position - transform.position).normalized;
		Vector2 origin = transform.position + direction * 1.5f;

		hit = Physics2D.Raycast(origin, direction, sightRange);
		Debug.DrawRay(origin, direction * sightRange, Color.red);

		if(!hit.collider){
			//Debug.Log("No collider hit by RayCast...");
			return false;
		}
		//return true;
		//Debug.Log("Tag of collider hit: " + hit.transform.tag);
		return hit.transform.tag == "Player";
	}

	// determines the special action taken if in range of the player, overwritten in children
	private void special() {
		Debug.Log("Activating Special.");
		// TODO: Code for "Game Over"
	}
}
