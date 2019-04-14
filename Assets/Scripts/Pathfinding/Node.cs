using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 * Uses the A* pathfinding algorithm
 * Used https://www.youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW by Sebastian Lague for reference
*/

public class Node : IHeapItem<Node> {
    public bool walkable;
    public Vector2 position;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;

    public Node(bool walkable, Vector2 position, int gridX, int gridY) {
        this.walkable = walkable;
        this.position = position;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost
    {
        get {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }

        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other) {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}