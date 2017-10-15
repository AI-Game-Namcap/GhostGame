using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrapper class for Node objects, for use in linked lists for pathing as well as A* itself
public class NodeWrapper {

	public Node thisNode; // node in the grid to which this wrapper reffers

	public NodeWrapper nextNode, parentNode; // links to other wrappers for structures/algorithms

	public int g, h, f; // values for A* algorithm

	public NodeWrapper(Node thisOne, NodeWrapper parent = null, NodeWrapper nextOne = null) {
		thisNode = thisOne;
		parentNode = parent;
		nextNode = nextOne;

		g = h = f = 0;
	}
}
