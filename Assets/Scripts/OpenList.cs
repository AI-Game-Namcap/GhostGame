using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the open list of nodes for A*, implemented with a min-heap
public class OpenList {

    private NodeWrapper[] elements;
    private int size;

    public OpenList (int n) {
		elements = new NodeWrapper[n];
        size = n;
	}

    // adds elements, maintaining heap property
	public void push ( NodeWrapper newElement ) {

		size ++;
		
		elements[size] = newElement;
		
		siftUp(size);
	}
	
	// removes root element from heap
	public NodeWrapper pop () {
		//return null if no elements in heap
		if (size<1)
			return null;
		
		NodeWrapper temp = elements[1];
		
		elements[1] = elements[size];
		size--;
		
		heapify(1);
		
		return temp;
	}

    private void heapify ( int localRoot ) {
		// find smallest existing child
		int leftChild = localRoot * 2;
		
		// if no children, return
		if( leftChild > size )
			return;

		int rightChild = leftChild + 1;
		// smallest child set to left by default
		int smallestChild = leftChild;
		
		// if there exists a right child, compare for smallest
		if( !(rightChild > size) ) {
			if( elements[rightChild].lessThan(elements[leftChild]) )
				smallestChild = rightChild;
		}
		
		// if smallestChild less than localRoot, swap
		if( elements[smallestChild].lessThan(elements[localRoot]) ){
			NodeWrapper temp = elements[localRoot];
			elements[localRoot] = elements[smallestChild];
			elements[smallestChild] = temp;
		
			// if the swapped smallestChild is also a parent, continue to heapify
			if( smallestChild <= (size*2) )
				heapify(smallestChild);
		}
	}

    // creates a heap property from bottom-up
	private void siftUp ( int index ) {
		// return if at root
		if ( index <= 1 )
			return;
		
		// get parent index
		int parentIndex = index / 2;
		
		// swap with parent, if smaller, and recursive call
		if( elements[index].lessThan(elements[parentIndex]) ) {
			NodeWrapper temp = elements[parentIndex];
			elements[parentIndex] = elements[index];
			elements[index] = temp;
			
			siftUp(parentIndex);
		}
	}
}