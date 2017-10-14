﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	[SerializeField] private NodeGrid grid; // the grid of all nodes in the play area
	[SerializeField] private RedTile red = null; // own red tile script, if it has one
	private int x, y; // position relative to other nodes in the grid, and node's size

	// Use this for initialization
	void Start () {
		x = (int)(transform.position.x / transform.localScale.x);
		y = (int)(transform.position.y / transform.localScale.y);

		grid.add(this, x, y); // add self to the node grid
	}

	public RedTile getRedTile() {
		return red;
	}

	public Vector3 getPosition() {
		return transform.position;
	}
}
