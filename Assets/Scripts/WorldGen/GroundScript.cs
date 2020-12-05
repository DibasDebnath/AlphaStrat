using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public int xGridIndex;
    public int yGridIndex;
    public bool isEmpty;
    public GameObject Glow1;

    public void SetGridData(int t_xGridIndex, int t_yGridIndex)
    {
        xGridIndex = t_xGridIndex;
        yGridIndex = t_yGridIndex;
        isEmpty = true;
    }
    public void SetGridIndex(int t_xGridIndex, int t_yGridIndex)
    {
        xGridIndex = t_xGridIndex;
        yGridIndex = t_yGridIndex;
        
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
        return this.transform.position;
    }

    public void GlowOneEnable()
    {
        Glow1.SetActive(true);
    }
    public void GlowOneDisable()
    {
        Glow1.SetActive(false);
    }
}
