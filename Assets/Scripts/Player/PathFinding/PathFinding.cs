using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;

    private GroundScript groundScript;

    private List<PathNode> openList;
    private List<PathNode> closedList;


    private List<GameObject> FindPath(GameObject startGround, GameObject endGround )
    {
        PathNode startNode = new PathNode(startGround.GetComponent<GroundScript>());
        PathNode endNode = new PathNode(endGround.GetComponent<GroundScript>());

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();


        for (int i=0; i < RefHolder.instance.worldGen.xTileCount; i++)
        {
            for (int j = 0; j < RefHolder.instance.worldGen.zTileCount; j++)
            {
                PathNode pathNode = new PathNode(RefHolder.instance.worldGen.getGridGroundObject(i,j).GetComponent<GroundScript>());
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;


            }
        }

        startNode.gCost = 0;
        startNode.hCost = calculateDistance(startNode,endNode);


        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                //REached End
                List<PathNode> calculatedPath =  calculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);


            //get naibours
        }



        
    }
    private int calculateDistance(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.i - b.i);
        int yDistance = Mathf.Abs(a.j - b.j);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestCostFNode = pathNodeList[0];

        for (int i =1; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestCostFNode.fCost)
            {
                lowestCostFNode = pathNodeList[i];
            }
        }
        return lowestCostFNode;
    }

    private List<PathNode> calculatePath(PathNode endNode)
    {
        return null;
    }



    private List<PathNode> getNeighbours(PathNode currentNode)
    {

        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.i -1 >= 0)
        {
            //Left 
            neighbourList.Add(GetPathNode(currentNode.i - 1, currentNode.j));
            //Left Down
            if(currentNode.j-1 >= 0) neighbourList.Add(GetPathNode(currentNode.i-1,currentNode.j -1));
            //Left Up
            if (currentNode.j + 1 < WorldGen.instance.zTileCount) neighbourList.Add(GetPathNode(currentNode.i - 1, currentNode.j + 1));


        }
    }

    private PathNode GetPathNode(int i, int j)
    {
        return  new PathNode(RefHolder.instance.worldGen.getGridGroundObject(i, j).GetComponent<GroundScript>());
    }
}
