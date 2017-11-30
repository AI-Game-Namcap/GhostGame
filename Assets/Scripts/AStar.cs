using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class AStar {

    private OpenList open;
    private ClosedList closed;
    private int x, y, g;

    private NodeGrid grid;

    private NodeWrapper currentNode, temp;

    public AStar (NodeGrid _grid) {
        grid = _grid;

        x = grid.getX();
        y = grid.getY();

        open = new OpenList((x+y)*3);
        //Debug.Log("Making open List of " + ((x+y)*3) + " spaces.");
        closed = new ClosedList(x, y);
    }

    // A* algorithm
    public NodeList findPath(int startX, int startY, int endX, int endY) {

        // reset data from last calculation
        open.reset();
        closed.reset();

        // 1: add starting node to open list
        open.push(new NodeWrapper(grid.getNode(startX, startY)));

        // loop
        while(true) {
            // 2: remove node from open list, set as current
            currentNode = open.pop();

            // return null if current is empty no path possible, return null
            if(currentNode == null)
                return null;

            x = currentNode.x;
            y = currentNode.y;

            // 3: check if current node is the goal, exit if it is~
            if(currentNode.equals(endX, endY))
                return drawPath(currentNode);

            // 4: generate neighbors, and add them to and update openlist
            // calculate g
            g = currentNode.g + 1;

            // top
            if(grid.inBounds(x, y+1) && grid.getNode(x, y+1).isTraversable())
                update(x, y+1, g, (Mathf.Abs(x-endX) + Mathf.Abs(y-endY+1)), currentNode);
            // top right
            if(grid.inBounds(x+1, y+1) && grid.getNode(x+1, y+1).isTraversable())
                update(x+1, y+1, g, (Mathf.Abs(x-endX+1) + Mathf.Abs(y-endY+1)), currentNode);
            // right
            if(grid.inBounds(x+1, y) && grid.getNode(x+1, y).isTraversable())
                update(x+1, y, g, (Mathf.Abs(x-endX+1) + Mathf.Abs(y-endY)), currentNode);
            // bottom right
            if(grid.inBounds(x+1, y-1) && grid.getNode(x+1, y-1).isTraversable())
                update(x+1, y-1, g, (Mathf.Abs(x-endX+1) + Mathf.Abs(y-endY-1)), currentNode);
            // bottom
            if(grid.inBounds(x, y-1) && grid.getNode(x, y-1).isTraversable())
                update(x, y-1, g, (Mathf.Abs(x-endX) + Mathf.Abs(y-endY-1)), currentNode);
            // bottom left
            if(grid.inBounds(x-1, y-1) && grid.getNode(x-1, y-1).isTraversable())
                update(x-1, y-1, g, (Mathf.Abs(x-endX-1) + Mathf.Abs(y-endY-1)), currentNode);
            // left
            if(grid.inBounds(x-1, y) && grid.getNode(x-1, y).isTraversable())
                update(x-1, y, g, (Mathf.Abs(x-endX-1) + Mathf.Abs(y-endY)), currentNode);
            // top left
            if(grid.inBounds(x-1, y+1) && grid.getNode(x-1, y+1).isTraversable())
                update(x-1, y+1, g, (Mathf.Abs(x-endX-1) + Mathf.Abs(y-endY+1)), currentNode);

            // 5: add current node to closed list
            currentNode.closed = true;
        }
    }

    // check if this node is in the open list, update if new g is lower
    private void update(int x, int y, int g, int h, NodeWrapper parent) {
        // do nothing if it's on the closed list
        if(closed.nodes[x, y].closed)
            return;
        // check if current node is in the open list
        if(closed.nodes[x, y].thisNode != null) {
            // do nothing if node in openlist's g is lower 
            if(closed.nodes[x, y].g <= g)
                return;
            // otherwise update it's parent, and g
            closed.nodes[x, y].g = g;
            closed.nodes[x, y].parentNode = parent;
        }
        // if not in list, add it
        else {
            NodeWrapper temp = new NodeWrapper(grid.getNode(x,y), parent, null, g, h);
            closed.nodes[x,y] = temp;
            open.push(temp);
        }
    }

    // 
    private NodeList drawPath(NodeWrapper current) {
        NodeList path = new NodeList();

        while(true) {
            path.push(current);
            //Debug.Log("Adding node " + current.thisNode.getPosition() + " to path.");
            current = current.parentNode;
            if(current == null)
                return path;
        }
    }
}