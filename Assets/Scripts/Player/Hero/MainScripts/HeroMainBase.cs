using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMainBase : MonoBehaviour
{

    public HeroSOBase heroSO;


    public bool isSelected;


    private void Awake()
    {
        heroSO.MainClass = this;
    }

    private void Start()
    {
        heroSO.PrintFromMain();
    }

    public void OnSelect()
    {
        heroSO.OnSelect();
        
        
    }

    public void OnDeselect()
    {
        heroSO.OnDeselect();
    }

    public void PrintSomething()
    {
        Debug.LogError("From Main Class");
    }

   
    


}
