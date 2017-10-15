using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the closed list of nodes for A*
public class ClosedList {

    private bool[,] nodes;

    public ClosedList(int x, int y) {
        nodes = new bool[x,y];

        // set all to false
        for(int i=0; i<x; i++) {
            for(int j=0; j<y; j++)
                nodes[i,j] = false;
        }
    }

    // "add" a node to the close list, make it's flag true
    public void add(int x, int y) {
        nodes[x,y] = true;
    }

    // see if a node's flag is 'true'
    public bool isInList(int x, int y) {
        return nodes[x,y];
    }
}