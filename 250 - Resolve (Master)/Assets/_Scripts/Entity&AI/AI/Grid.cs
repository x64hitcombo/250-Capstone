using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPosition;
    public LayerMask unwalkable;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float Distance;

    Node[,] grid;
    public List<Node> path;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); // 30 / 2 = 15
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); // 30 / 2 = 15
        CreateGrid();
    }

    private void Update()
    {

    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool wall = true;
                if (Physics.CheckSphere(worldPoint, nodeRadius, unwalkable))
                {
                    wall = false;
                }

                grid[x, y] = new Node(wall, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 _worldPosition)
    {
        if (grid != null)
        {
            float percentX = 0;
            float percentY = 0;
            if (_worldPosition.x < 0)
            {
                percentX = ((_worldPosition.x + gridWorldSize.x) + (gridWorldSize.x / 2)) / gridWorldSize.x;
            }
            else if (_worldPosition.x >= 0)
            {
                percentX = (((_worldPosition.x + gridWorldSize.x) / 2) / gridWorldSize.x);
            }

            if (_worldPosition.z < 0)
            {
                percentY = ((_worldPosition.z + (gridWorldSize.y / 2)) / gridWorldSize.y) * -1;
            }
            else if (_worldPosition.z >= 0)
            {
                percentY = ((_worldPosition.z + (gridWorldSize.y / 2)) / gridWorldSize.y);
            }

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

            return grid[x, y];
        }
        return null;
    }

    public Node NodeFromGridPosition(Vector3 _gridPosition)
    {
        if (grid != null)
        {
            //Assuming Start Position is at x:-25 y:1 z:3
            float percentX = 0;
            float percentY = 0;
            if (_gridPosition.x < 0)
            {
                percentX = ((_gridPosition.x + gridWorldSize.x) + (gridWorldSize.x / 2)) / gridWorldSize.x; // ((-25 + 30) + (30 / 2)) / 30 = 0.66
            }
            else if (_gridPosition.x >= 0)
            {
                percentX = (((_gridPosition.x + gridWorldSize.x) / 2) / gridWorldSize.x); // ((-25 + 30) / 2) / 30) = -0.33
            }
             
            if (_gridPosition.z < 0 )
            {
                percentY = ((_gridPosition.z + gridWorldSize.y) + (gridWorldSize.y / 2)) / gridWorldSize.y; // ((-5 + 30) + (30 / 2)) / 30 = 1.33
            }
            else if (_gridPosition.z >= 0)
            {
                percentY = ((_gridPosition.z + (gridWorldSize.y / 2)) / gridWorldSize.y); // ((5 + (30 / 2)) / 30) = 0.66
            }
            

            percentX = Mathf.Clamp01(percentX); // 0.33
            percentY = Mathf.Clamp01(percentY); // 0.66

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX); // (15 - 1) * 0.33 = 5
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY); // (15 - 1) * 0.66 = 9

            return grid[x, y]; // return grid[5, 9]
        }
        return null;
    }

    public List<Node> GetNeighborNodes(Node _node)
    {
        List<Node> neighborNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //Right Side
        xCheck = _node.gridX + 1;
        yCheck = _node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add(grid[xCheck, yCheck]);
            }
        }


        //Left Side
        xCheck = _node.gridX - 1;
        yCheck = _node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Top Side
        xCheck = _node.gridX;
        yCheck = _node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Bottom Side
        xCheck = _node.gridX;
        yCheck = _node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add(grid[xCheck, yCheck]);
            }
        }

        /*
        //Top-Left Side
        xCheck = _node.gridX - 1;
        yCheck = _node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Bottom-Left Side
        xCheck = _node.gridX - 1;
        yCheck = _node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Top-Right Side
        xCheck = _node.gridX + 1;
        yCheck = _node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Bottom-Right Side
        xCheck = _node.gridX + 1;
        yCheck = _node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeY)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNodes.Add(grid[xCheck, yCheck]);
            }
        }
        */

        return neighborNodes;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0.2f, gridWorldSize.y));
        //Node aiNode = NodeFromWorldPosition(startPosition.position);
        //Test aiNode below
        Node aiNode = NodeFromGridPosition(startPosition.position);

        print(aiNode.gridX);
        print(aiNode.gridY);

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (node.walkable)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if (path != null)
                {
                    if (path.Contains(node))
                    {
                        Gizmos.color = Color.red;
                    }
                }

                if (aiNode != null)
                {
                    if (aiNode == node)
                    {
                        Gizmos.color = Color.cyan;
                    }
                }

                Gizmos.DrawCube(node.position, new Vector3(1 * (nodeDiameter - Distance), 0.2f, 1 * (nodeDiameter - Distance)));
            }
        }
    }
}
