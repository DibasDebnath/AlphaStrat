using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;

    private List<PathNode> allPathNodes = new List<PathNode>();



    public List<GameObject> GetPathObjects(GameObject startGround , GameObject endGround)
    {
        List<PathNode> pathNodes = FindPath(startGround.GetComponent<GroundScript>().xGridIndex, startGround.GetComponent<GroundScript>().yGridIndex, endGround.GetComponent<GroundScript>().xGridIndex, endGround.GetComponent<GroundScript>().yGridIndex);

        List<GameObject> pathObjects = new List<GameObject>();

        for(int i = 0; i < pathNodes.Count; i++)
        {
            pathObjects.Add(RefHolder.instance.worldGen.getGridGroundObject(pathNodes[i].x, pathNodes[i].x));
        }

        return pathObjects;
    }

    



    
    private List<PathNode> FindPath(int startX , int startY , int endX,int endY)
    {
        allPathNodes.Clear();
        for (int i = 0; i < RefHolder.instance.worldGen.xTileCount; i++)
        {
            for (int j = 0; j < RefHolder.instance.worldGen.zTileCount; j++)
            {
                allPathNodes.Add(new PathNode(i, j, RefHolder.instance.worldGen.getGridGroundObject(i,j).GetComponent<GroundScript>().isEmpty));
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

        for (int x = 0; x < RefHolder.instance.worldGen.xTileCount; x++)
        {
            for (int y = 0; y < RefHolder.instance.worldGen.zTileCount; y++)
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
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (currentNode.y + 1 < RefHolder.instance.worldGen.zTileCount) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < RefHolder.instance.worldGen.xTileCount)
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (currentNode.y + 1 < RefHolder.instance.worldGen.zTileCount) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < RefHolder.instance.worldGen.zTileCount) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

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
}
