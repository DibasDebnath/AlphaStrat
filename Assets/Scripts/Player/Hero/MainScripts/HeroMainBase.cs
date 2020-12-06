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

    

    public void SetMapIndex(int xGridIndex,int yGridIndex)
    {
        this.xGridIndex = xGridIndex;
        this.yGridIndex = yGridIndex;
    }

    public void StartMoveToCorotine(List<GameObject> path,int xIndex,int yIndex)
    {
        StartCoroutine(heroSO.MoveToTileEnumerator(path, xIndex,yIndex));
    }
    


}
