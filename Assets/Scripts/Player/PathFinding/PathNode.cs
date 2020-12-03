using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GroundScript groundScript;

    public int i;
    public int j;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;

    public PathNode(GroundScript t_groundScript)
    {
        this.groundScript = t_groundScript;
        this.i = groundScript.xGridIndex;
        this.j = groundScript.yGridIndex;
    }


    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public override string ToString()
    {
        return i+","+j;
    }



}
