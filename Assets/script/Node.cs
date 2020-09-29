using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX, gridY;
    public int gCost, hCost;

    public Node parent;
    public Node(bool _walkable, Vector3 _worldPos, int _gridx,int _gridy)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridx;
        gridY = _gridy;
    }
    public int fCost
    {
        get { return gCost + hCost; }
        
    }
}