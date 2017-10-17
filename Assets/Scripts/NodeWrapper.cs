using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrapper class for Node objects, for use in linked lists for pathing as well as A* itself
public class NodeWrapper {

	public Node thisNode; // node in the grid to which this wrapper reffers

	public NodeWrapper nextNode, parentNode; // links to other wrappers for structures/algorithms

	public int x, y, g, h; // values for A* algorithm

	public bool closed; // if the contained nodes is closed or open

	public NodeWrapper(Node thisOne = null, NodeWrapper parent = null, NodeWrapper nextOne = null, 
							int _g = 0, int _h = 0) {
		thisNode = thisOne;
		parentNode = parent;
		nextNode = nextOne;

		if(thisOne != null) {
			x = thisNode.getX();
			y = thisNode.getY();
		}
		else {
			x = y = 0;
		}

		g = _g;
		h = _h;

		closed = false;
	}

	public bool equals(int CX, int CY) {
		return (x == CX && y == CY);
	}

	// compare f-scores, return true if this one's less
	public bool lessThan(NodeWrapper compare) {
		return (g + h) < (compare.g + compare.h);
	}
}
