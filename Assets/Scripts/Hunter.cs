using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {

	[SerializeField] private Transform player; // player's location

	//[SerializeField] private Rigidbody2D playerBody; // player's Rigidbody

	[SerializeField] private NodeGrid grid; // grid of nodes

	[SerializeField] private Rigidbody2D body; // rigid body for movement

	[SerializeField] private float sightRange = 15f; // how far it can see the player

	[SerializeField] private float moveSpeed = 2.5f; // patrol movement speed, alert = 2x this

	[SerializeField] private float moveRadus = 0.25f; // distance to the destination target will stop at

	[SerializeField] private float abilityRadius = 1.5f; // rangeat which the Hunter uses it's ability
	
	[SerializeField] private int reactionTime = 10; // number of A* calls to ignore

	[SerializeField] private Node[] patrolPoints; // patrol points to walk when in patrol state 
	private NodeList patrolList; // Patrol Points traslated into a NodeList

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
		alert = sight = false;
		starCounter = reactionTime;
		aStar = new AStar(grid);
		makePatrol();
		moveList = patrolList;
	}
	
	
	void FixedUpdate() {
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
				alert = true; // make alert status dependant on hunter's reaction time
				//Debug.Log("Genrating A* path to player position...");
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
				// moveList.clear();
				sight = false;
				// A* to location player is heading towards (if able)
				/*moveList = aStar.findPath((int)transform.position.x, (int)transform.position.y,
											(int)(player.position.x + playerBody.velocity.x), 
											(int)(player.position.y + playerBody.velocity.y));*/
				// Alternate: A* to the player's current location (if able)
				//moveList = aStar.findPath((int)transform.position.x, (int)transform.position.y,
				//							(int)player.position.x, (int)player.position.y);
				//starCounter = reactionTime;
			}
		}

		// if list is empty or missing, and no LOS, go to patrol
		if(!sight && (moveList == null || moveList.isEmpty())) {
			Debug.Log("Move list is empty and no LOS, generating patrol path...");
			// deactivate alert status
			alert = false;

			// if there is no patrol list, make one
			if (patrolList.isEmpty()) {
				Debug.Log("Patrol list empty, creating new list from points...");
				makePatrol();
			}

			//moveList = patrolList;
			
			// if we're at the first node in the patrol list, follow patrol list
			if ((patrolList.peek().getPosition() - transform.position).magnitude < 2f) {
				//patrolList.pop();
				moveList = patrolList;
			}
			// otherwise, A* to the first point in the list
			else {
				Debug.Log("Genrating A* path to patrol point...");
				moveList = aStar.findPath((int)transform.position.x, (int)transform.position.y,
											patrolList.peek().getX(), patrolList.peek().getY());
			}
			
		}
		 
		if(moveList == null) {
			moveList = new NodeList();
			Debug.Log("Null MoveList replaced with blank");
		}
		
		Debug.Log("Destination is: " + destination);
		vector = destination - transform.position;
		// if position is too far from 'destination' move
		if ((vector).magnitude > moveRadus) {
			// move, capping movement speed
			if(vector.magnitude > moveSpeed || vector.magnitude < 1f) { // TODO: setting move speeds
				vector.Normalize();
				vector *= moveSpeed;
			}
			// double speed if alert
			if(alert) 
				vector *= 2f;

			body.velocity = vector;
		}
		// else get next position from NodeList
		else {
			if(!moveList.isEmpty()) {
				destinationNode = moveList.pop();
				destination = destinationNode.getPosition();
			}
		}
	}

	// translate patrol path array into the NodeList patrolList
	private void makePatrol() { 
		patrolList = new NodeList();
		for (int i = (patrolPoints.Length - 1); i >= 0; i--) {
			patrolList.push(new NodeWrapper(patrolPoints[i]));
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
		//Debug.Log("Tag of collider hit: " + hit.transform.tag);
		return hit.transform.tag == "Player";
	}

	// determines the special action taken if in range of the player, overwritten in children
	private void special() {
		Debug.Log("Activating Special.");
		// TODO: Code for "Game Over"
	}
}
