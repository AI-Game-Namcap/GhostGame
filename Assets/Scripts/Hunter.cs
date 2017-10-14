using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {

	[SerializeField] private Transform player; // player's location

	[SerializeField] private float moveSpeed = 3f; // patrol movement speed, alert = 2x this
	private float moveRadus = 1.0f; // how close it will get to target position before stopping
	private NodeList moveList; // list of nodes to move to
	private Node destinationNode;
	private Vector3 destination;

	private bool alert;

	// Use this for initialization
	void Start () {	
		alert = false;
	}
	
	
	void FixedUpdate () {
		// if position is too far from 'destination' move
		if ((destination - transform.position).magnitude > moveRadus) {
			// move
		}
		// else get next position from NodeList
		else {
			destinationNode = moveList.nextNode();
			if (destinationNode != null) {
				destination = destinationNode.getPosition();
			}
			else {
				// determine node to go to
			}
		}
		
		// LOS check
	}

	// determine if has player in line of sight, overwritten in children
	private bool lineOfSight() {
		return false;
	}

	// determines the special action taken if in range of the player, overwritten in children
	private void special() {
		
	}
}
