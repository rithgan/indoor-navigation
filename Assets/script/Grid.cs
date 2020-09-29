using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public InputField input;
    public Transform player;
    public Transform player2;
    public Vector2 gridWorldSize;
    public LayerMask unwalkableMask;
    public float nodeRadius;
    Node[,] grid;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    private GameObject plane1;
    Vector3 vec = new Vector3(1, 1, 1);
    Vector3 vec2 = new Vector3(1, 1, 1);
    string tec;
    int h = 1;
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    void CreateGrid()
    {

        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - (Vector3.right * (gridWorldSize.x / 2) + Vector3.forward * (gridWorldSize.y / 2));

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
        if (player.position != vec||player2.position !=vec2||input.text !=tec)
        {    if (h == 0)
            {
                Debug.Log(tec);
                GameObject[] prefabs = GameObject.FindGameObjectsWithTag("nice");
                foreach (GameObject prefab in prefabs)
                    Destroy(prefab);
               
               
            }
            Draw();
            //Debug.Log(vec);
            vec2 = player2.position;
            vec = player.position;
            tec = input.text;
            h = 0;
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for(int x=-1;x<=1;x++)
        {
            for(int y=-1;y<=1;y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if(checkX>=0&&checkX<gridSizeX&&checkY>=0&&checkY<gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }

            }
        }

        return neighbours;

    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];




    }
    public List<Node> path;
   /* public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);

            foreach (Node n in grid)
            {

                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (path != null)
                    if (path.Contains(n))
                    {
                        
                        Gizmos.color = Color.green;
                    }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .0001f));
            }
        }
    }*/
    public void Draw()
    {

        foreach (Node n in path)
        {
            // Debug.Log(n.worldPosition);

            plane1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
           
            plane1.transform.position = new Vector3(n.worldPosition.x, 2, n.worldPosition.z);
            plane1.transform.localScale = new Vector3(1, 1, 1);
            plane1.tag = "nice";
           
            // vec = player.position;
        }
    }
    
    void Update()
    {
        CreateGrid();
        
    }
}

