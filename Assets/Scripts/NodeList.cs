using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stack of node objects for pathing, in wrappers
public class NodeList {
	private NodeWrapper head;

	public NodeList(NodeWrapper first = null) {
		head = first;
	}

	public Node pop() {
		if(head != null) {
			Node temp = head.thisNode;
			head = head.nextNode;
			return temp;
		}
		else
			return null;
	}
	
	// add a node to the front of the list
	public void push(NodeWrapper newNode) {
		if(head != null) 
			newNode.nextNode = head;
		head = newNode;
	}

	// empties the list
	public void clear() {
		head = null;
	}
}
