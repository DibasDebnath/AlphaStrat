using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public int xGridIndex;
    public int yGridIndex;
    public bool isEmpty;
    Vector3 gridPosition;

    public void SetGridData(int t_xGridIndex, int t_yGridIndex, Vector3 t_gridPosition)
    {
        xGridIndex = t_xGridIndex;
        yGridIndex = t_yGridIndex;
        gridPosition = t_gridPosition;
        isEmpty = true;
    }
    public void SetGridEmpty()
    {
        isEmpty = true;
    }
    public void SetGridNotEmpty()
    {
        isEmpty = false;
    }
    public bool GetGridIsEmpty()
    {
        return isEmpty;
    }
    public Vector3 GetGridPosition()
    {
        return gridPosition;
    }
}
