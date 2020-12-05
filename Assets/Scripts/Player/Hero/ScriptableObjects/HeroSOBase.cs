using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroSOBase : ScriptableObject
{
    public string heroName;
    public string heroType;

    [Range(0.0f, 100.0f)]
    public float health;
    [Range(0.0f, 100.0f)]
    public float attack;
    [Range(0.0f, 100.0f)]
    public float defence;
    [Range(0.0f, 100.0f)]
    public float heal;
    [Range(0f, 10f)]
    public int range;


    private HeroMainBase mainClass;

    

    private bool isSelected;

    public HeroMainBase MainClass { get => mainClass; set => mainClass = value; }

    private void Awake()
    {
        isSelected = false;
        MainClass = null;
    }

    #region Main Turn Methodes 

    public void OnSelect()
    {
        if (!isSelected)
        {
            isSelected = true;
            HighlightMovableTiles();
        }
        
    }

    public void OnDeselect()
    {
        isSelected = false;
        DeHighlightMovableTiles();
    }

    public void HighlightMovableTiles()
    {
        
    }

    public void DeHighlightMovableTiles()
    {

    }

    public void HighlightAttackableEnemy()
    {

    }
    public void DeHighlightAttackableEnemy()
    {

    }

    public void OnSelectTile()
    {
        
    }
    public void ConfirmSelectedTile()
    {

    }

    public void CancelSelectedTile()
    {

    }

    public void MoveToSelectedTile()
    {

    }

    public virtual void PrintStuff()
    {
        Debug.LogError("Not Works");
    }

    public void PrintFromMain()
    {
        MainClass.PrintSomething();
    }
    #endregion



}
