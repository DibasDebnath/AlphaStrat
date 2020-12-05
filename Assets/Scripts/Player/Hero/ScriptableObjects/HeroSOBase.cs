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

    private List<GameObject> highlightedGrounds;

    

    public HeroMainBase MainClass { get => mainClass; set => mainClass = value; }
    public List<GameObject> HighlightedGrounds { get => highlightedGrounds; set => highlightedGrounds = value; }

    private void Awake()
    {
        
        MainClass = null;
        HighlightedGrounds = null;
    }

    #region Main Turn Methodes 

    public void OnSelect()
    {
        
        HighlightMovableTiles();
        
        
    }

    public void OnDeselect()
    {
        
        DeHighlightMovableTiles();
    }

    public void HighlightMovableTiles()
    {
        HighlightedGrounds = RefHolder.instance.pathFinding.GetPathObjectsInRange(MainClass.xGridIndex,MainClass.yGridIndex,range);

        for(int i = 0; i < HighlightedGrounds.Count; i++)
        {
            HighlightedGrounds[i].transform.GetComponent<GroundScript>().GlowOneEnable();
        }
    }

    public void DeHighlightMovableTiles()
    {
        HighlightedGrounds = RefHolder.instance.pathFinding.GetPathObjectsInRange(MainClass.xGridIndex, MainClass.yGridIndex, range);

        for (int i = 0; i < HighlightedGrounds.Count; i++)
        {
            HighlightedGrounds[i].transform.GetComponent<GroundScript>().GlowOneDisable();
        }
        HighlightedGrounds = null;
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



    #region Support Methods

    public bool IsGroundHighlighted(int xGridIndex, int yGridIndex)
    {
        bool highlighted = false;
        if(highlightedGrounds == null)
        {
            return false;
        }
        for (int i = 0; i < HighlightedGrounds.Count; i++)
        {
            
            if (HighlightedGrounds[i].transform.GetComponent<GroundScript>().xGridIndex == xGridIndex && HighlightedGrounds[i].transform.GetComponent<GroundScript>().yGridIndex == yGridIndex)
            {
                highlighted = true;
                break;
            }
        }
        return highlighted;
    }

    #endregion

}
