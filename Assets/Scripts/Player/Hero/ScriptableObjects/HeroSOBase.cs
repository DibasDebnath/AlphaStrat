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

    

    

    public HeroMainBase MainClass { get => mainClass; set => mainClass = value; }

    private void Awake()
    {
        MainClass.isSelected = false;
        MainClass = null;
    }

    #region Main Turn Methodes 

    public void OnSelect()
    {
        if (!MainClass.isSelected)
        {
            MainClass.isSelected = true;
            HighlightMovableTiles();
        }
        
    }

    public void OnDeselect()
    {
        MainClass.isSelected = false;
        DeHighlightMovableTiles();
    }

    public void HighlightMovableTiles()
    {
        List<GameObject> grounds = RefHolder.instance.pathFinding.GetPathObjectsInRange(MainClass.xIndex,MainClass.yIndex,range);

        for(int i = 0; i < grounds.Count; i++)
        {
            grounds[i].transform.GetComponent<GroundScript>().GlowOneEnable();
        }
    }

    public void DeHighlightMovableTiles()
    {
        List<GameObject> grounds = RefHolder.instance.pathFinding.GetPathObjectsInRange(MainClass.xIndex, MainClass.yIndex, range);

        for (int i = 0; i < grounds.Count; i++)
        {
            grounds[i].transform.GetComponent<GroundScript>().GlowOneDisable();
        }
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

    
    #endregion



}
