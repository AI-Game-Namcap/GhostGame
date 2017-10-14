using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrapper class for Node objects, for use in linked lists for pathing
public class NodeWrapper {
	public Node thisNode;
	public NodeWrapper nextNode;

	public NodeWrapper(Node thisOne, NodeWrapper nextOne = null) {
		thisNode = thisOne;
		nextNode = nextOne;
	}
}
