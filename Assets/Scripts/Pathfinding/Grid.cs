using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public LayerMask unwalkableMask;
    public GameMap gameMap;
    public Vector2 gridSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start() {
        

        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForEndOfFrame();

        gridSize = gameMap.size;
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 bottomLeft = (Vector2)transform.position - Vector2.right * gridSize.x / 2 - Vector2.up * gridSize.y / 2;

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition) {
        float percentX = worldPosition.x / gridSizeX + 0.5f;
        float percentY = (worldPosition.y + gridSize.y / 2) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        
        return grid[x, y];
    }

    public List<Node> path = new List<Node>();
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1));

        if (grid != null) {
            foreach (Node n in grid) {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                Gizmos.DrawCube(n.position, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }
}