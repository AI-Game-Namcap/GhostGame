using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {

	[SerializeField] private Transform player; // player's location

	[SerializeField] private NodeGrid grid; // grid of nodes

	[SerializeField] private RigidBody2D body; // rigid body for movement

	[SerializeField] private float sightRange; // how far it can see the player

	[SerializeField] private float moveSpeed = 3f; // patrol movement speed, alert = 2x this

	[SerializeField] private float moveRadus = 0.25f; // distance to the destination target will stop at

	[SerializeField] private float abilityRadius = 1.5f; // rangeat which the Hunter uses it's ability
	
	[SerializeField] private int reactionTime = 10; // number of A* calls to ignore

	private float moveRadus = 1.0f; // how close it will get to target position before stopping
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
		moveList = new NodeList();
		alert = sight = false;
		starCounter = reactionTime;
		aStar = new aStar(grid);
	}
	
	
	void FixedUpdate () {
		vector = destination - transform.position
		// if position is too far from 'destination' move
		if ((vector).magnitude > moveRadus) {
			// move, capping movement speed
			if(vector.magnitude > moveSpeed) {
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
		if(lineOfSight)
		{
			sight = true;
			// if within radius, use special, don't update moves
			if((player - transform.position).magnitude < abilityRadius) {
				special();
			}
			// A* to player
			else if(starCounter < 1) {
				moveList = aStar.findPath(grid.getNode(0,0).convertX(transform.position.x),
										  grid.getNode(0,0).convertY(transform.position.y),
										  grid.getNode(0,0).convertX(player.position.x),
										  grid.getNode(0,0).convertY(player.position.y));

				starCounter = reactionTime;
			}
			else
				starCounter--;
		}
		else{
			// LOS just lost
			if(sight) {
				// stop moving/empty move list
				moveList.clear();
				sight = false;
			}
		}
	}

	// determine if has player in line of sight, overwritten in children
	private bool lineOfSight() {
		bool tempBool;
		hit = Physics2D.Raycast(transform.position, player, sightRange);

		return hit.transform.position == player;
	}

	// determines the special action taken if in range of the player, overwritten in children
	private void special() {
		// TODO: Code for "Game Over"
	}
}
