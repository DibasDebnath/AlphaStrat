using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMainBase : MonoBehaviour
{

    public HeroSOBase heroSO;


    public bool isSelected;
    public int xIndex;
    public int yIndex;


    private void Awake()
    {
        heroSO.MainClass = this;
    }

    private void Start()
    {
       
    }

    public void OnSelect()
    {
        heroSO.OnSelect();
        
        
    }

    public void OnDeselect()
    {
        heroSO.OnDeselect();
    }

    

    public void setMapIndex(int xIndex,int yIndex)
    {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
    }
    


}
