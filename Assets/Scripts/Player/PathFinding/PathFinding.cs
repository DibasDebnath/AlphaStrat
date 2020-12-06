using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;

    private List<PathNode> allPathNodes = new List<PathNode>();

    private int xCount;
    private int yCount;

    public List<GameObject> GetPathObjects(int startX, int startY, int endX, int endY)
    {

        this.xCount = RefHolder.instance.worldGen.xTileCount;
        this.yCount = RefHolder.instance.worldGen.zTileCount;

        List<PathNode> pathNodes = FindPath(startX, startY, endX, endY, xCount, yCount);

        if(pathNodes == null)
        {
            Debug.LogError("Invalid Path");
            return null;
        }
        List<GameObject> pathObjects = new List<GameObject>();

        for(int i = 0; i < pathNodes.Count; i++)
        {
            pathObjects.Add(RefHolder.instance.worldGen.GetGridGroundObject(pathNodes[i].x, pathNodes[i].y));
        }

        return pathObjects;
    }

    public List<GameObject> GetPathObjectsInRange(int startX, int startY , int range)
    {
        this.xCount = RefHolder.instance.worldGen.xTileCount;
        this.yCount = RefHolder.instance.worldGen.zTileCount;

        List<PathNode> pathNodes = GetPathsInRange(startX, startY, range, xCount, yCount);

        if (pathNodes == null)
        {
            Debug.LogError("Invalid Range");
            return null;
        }

        List<GameObject> pathObjects = new List<GameObject>();

        for (int i = 0; i < pathNodes.Count; i++)
        {
            pathObjects.Add(RefHolder.instance.worldGen.GetGridGroundObject(pathNodes[i].x, pathNodes[i].y));
        }

        return pathObjects;
    }


    private List<PathNode> FindPath(int startX , int startY , int endX,int endY , int xCount, int yCount)
    {
        allPathNodes.Clear();
        for (int i = 0; i < xCount; i++)
        {
            for (int j = 0; j < yCount; j++)
            {
                allPathNodes.Add(new PathNode(i, j, RefHolder.instance.worldGen.GetGridGroundObject(i,j).GetComponent<GroundScript>().isEmpty));
            }
        }
        PathNode startNode = GetNode(startX, startY);
        PathNode endNode = GetNode(endX, endY);

        if (startNode == null || endNode == null)
        {
            // Invalid Path
            return null;
        }

        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(startNode);

        for (int x = 0; x < xCount; x++)
        {
            for (int y = 0; y < yCount; y++)
            {
                PathNode pathNode = GetNode(x, y);
                pathNode.gCost = 99999999;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();


        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                // Reached final node
               
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the openList
        return null;


    }

    private PathNode GetNode(int x, int y)
    {
        PathNode pathNode = null;
        for(int i = 0; i < allPathNodes.Count; i++)
        {
            if(allPathNodes[i].x == x && allPathNodes[i].y == y)
            {
                pathNode = allPathNodes[i];
                break;
            }
        }
        return pathNode;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0)
            {
                //Left and Down tile befor diagonal tile add
                if(GetNode(currentNode.x - 1, currentNode.y).isWalkable || GetNode(currentNode.x, currentNode.y - 1).isWalkable)
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
                }
            }

            // Left Up

            if (currentNode.y + 1 < yCount)
            {
                if (GetNode(currentNode.x - 1, currentNode.y).isWalkable || GetNode(currentNode.x, currentNode.y + 1).isWalkable)
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
                }
            }
            
        }
        if (currentNode.x + 1 < xCount)
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0)
            {
                if (GetNode(currentNode.x + 1, currentNode.y).isWalkable || GetNode(currentNode.x, currentNode.y - 1).isWalkable)
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
                }
            }
            // Right Up
            if (currentNode.y + 1 < yCount)
            {
                if (GetNode(currentNode.x + 1, currentNode.y).isWalkable || GetNode(currentNode.x, currentNode.y + 1).isWalkable)
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
                }
            }
            
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < yCount) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + STRAIGHT_COST * remaining;
    }



    private List<PathNode> GetPathsInRange(int startX, int startY, int range, int xCount, int yCount)
    {
        int xRangePlus = startX + range;
        if(xRangePlus >= xCount)
        {
            xRangePlus = xCount - 1;
        }
        int xRangeMinus = startX - range;
        if (xRangeMinus < 0)
        {
            xRangeMinus = 0;
        }

        int yRangePlus = startY + range;
        if (yRangePlus >= yCount)
        {
            yRangePlus = yCount - 1;
        }
        int yRangeMinus = startY - range;
        if (yRangeMinus < 0)
        {
            yRangeMinus = 0;
        }


        allPathNodes.Clear();
        for (int i = xRangeMinus; i <= xRangePlus; i++)
        {
            for (int j = yRangeMinus; j <= yRangePlus; j++)
            {
                if (RefHolder.instance.worldGen.GetGridGroundObject(i, j).GetComponent<GroundScript>().isEmpty == true)
                {
                    allPathNodes.Add(new PathNode(i, j, true));
                }
                
            }
        }


        return allPathNodes;
    }



}
