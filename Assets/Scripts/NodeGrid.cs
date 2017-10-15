using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour {

	[SerializeField] private int horizontal; // number of nodes across
	[SerializeField] private int vertical; // number of nodes top to bottom

	private Node[,] grid;

	void Awake () {
		grid = new Node[horizontal,vertical];
	}

	public void add(Node node, int x, int y) {
		grid[x,y] = node; 
	}

	public Node getNode(int x, int y) {
		return grid[x,y];
	}

	// return true if the x/y are in the bounds of the grid
	public bool inBounds(int x, int y) {
		if(x < 0 || y < 0 || x >= horizontal || y >= vertical)
			return false;
		return true;
	}

	// returns x/y dimentions
	public int getX() {
		return horizontal;
	}
	public int getY() {
		return vertical;
	}
}
