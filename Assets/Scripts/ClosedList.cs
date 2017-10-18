using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A HashMap for referencing both open and closed lists
public class ClosedList {

    // used to track all nodes, null = in closed list
    // .thisNode == null if unvisited
    public NodeWrapper[,] nodes;

    private int x, y;

    public ClosedList(int _x, int _y) {
        x = _x;
        y = _y;
        nodes = new NodeWrapper[x,y];
        reset();        
    }

    // sets all data to default state
    public void reset() {
        // set all to empty wrappers
        for(int i=0; i<x; i++) {
            for(int j=0; j<y; j++)
                nodes[i,j] = new NodeWrapper();
        }
    }
}