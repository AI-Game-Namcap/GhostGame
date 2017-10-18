using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {

	[SerializeField] private Transform player; // player's location

	[SerializeField] private NodeGrid grid; // grid of nodes

	[SerializeField] private Rigidbody2D body; // rigid body for movement

	[SerializeField] private float sightRange = 5f; // how far it can see the player

	[SerializeField] private float moveSpeed = 3f; // patrol movement speed, alert = 2x this

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
			//Debug.Log("Line of Sight Extablished");
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
				Debug.Log("Created New Path with A* from " + transform.position + " to " + player.position);
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
			}
		}
	}

	// determine if has player in line of sight, overwritten in children
	private bool lineOfSight() {
		Debug.DrawRay(transform.position, player.position-transform.position, Color.red);
		/*hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), 
								new Vector2(player.position.x, player.position.y), sightRange);

		if(!hit.collider){
			Debug.Log("No collider hit by RayCast...");
			return false;
		}*/
		return true;//hit.collider.CompareTag("Player");
	}

	// determines the special action taken if in range of the player, overwritten in children
	private void special() {
		Debug.Log("Activating Special.");
		// TODO: Code for "Game Over"
	}
}
