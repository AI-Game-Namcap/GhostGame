using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// list of node objects for pathing, in wrappers
public class NodeList {
	private NodeWrapper head, tail, current;

	public NodeList(NodeWrapper first = null) {
		current = head = tail = first;
	}

	// returns the node at the 'current' position, and advances the position, 
	// returns NULL if no current node
	public Node nextNode() {
		
		if(current != null) {
		Node temp = current.thisNode;

		current = current.nextNode;

		return temp;
		}
		else
			return null;
	}

	public void add(Node newNode) {
		NodeWrapper temp = new NodeWrapper(newNode);
		tail.nextNode = temp;
		tail = temp;
	}
}
