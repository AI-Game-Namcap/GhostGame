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

	// returns, but does not remove, the node at the top of the list
	public Node peek() {
		if (head != null)
			return head.thisNode;
		else
			return null;
	}

	// empties the list
	public void clear() {
		head = null;
	}

	// returns true if the list is empty
	public bool isEmpty() {
		return head == null;
	}
}
