using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    

    public int x;
    public int y;
    public bool isWalkable;
    public int gCost = 0;
    public int hCost;
    public int fCost = 0;

    public PathNode cameFromNode = null;

    public PathNode(int x, int y, bool isWalkable)
    {
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
    }


    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    



}
