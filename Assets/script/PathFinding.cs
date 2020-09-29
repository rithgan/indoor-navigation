using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    Grid grid;
    public Transform seeker, target; 
    void Awake()
    {
        grid = GetComponent<Grid>();
        
    }
    void Update()
        {
        FindPath(seeker.position, target.position);

    }
   void FindPath(Vector3 startPos,Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);
        while(openSet.Count>0)
        {
            Node currentNode = openSet[0];
            for(int i=1; i<openSet.Count;i++)
            {
                if(openSet[i].fCost < currentNode.fCost|| (openSet[i].fCost == currentNode.fCost && openSet[i].hCost<currentNode.hCost) )
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);
            if(currentNode== targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }
            foreach (Node neighbours in grid.GetNeighbours(currentNode))
            {
                if(!neighbours.walkable || closeSet.Contains(neighbours))
                {
                    continue;
                }
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbours);
                if(newMovementCostToNeighbour < neighbours.gCost || !openSet.Contains(neighbours))
                {
                    neighbours.gCost = newMovementCostToNeighbour;
                    neighbours.hCost = GetDistance(neighbours, targetNode);
                    neighbours.parent = currentNode;
                    if (!openSet.Contains(neighbours))
                        openSet.Add(neighbours);
                }
            }
        }

    }

    void RetracePath(Node startNode,Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while(currentNode !=startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;

        }
        path.Reverse();
        grid.path = path;
    }
    int GetDistance(Node nodeA,Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);

    }
}
