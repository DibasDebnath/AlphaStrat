using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMainBase : MonoBehaviour
{

    public HeroSOBase heroSO;


    
    public int xGridIndex;
    public int yGridIndex;


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

    

    public void setMapIndex(int xGridIndex,int yGridIndex)
    {
        this.xGridIndex = xGridIndex;
        this.yGridIndex = yGridIndex;
    }
    


}
