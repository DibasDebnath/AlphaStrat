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

    public int speed;

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

    public void MoveToSelectedTile(int xIndex , int yIndex)
    {
        List<GameObject> path = RefHolder.instance.pathFinding.GetPathObjects(MainClass.xGridIndex, MainClass.yGridIndex, xIndex, yIndex);
        if (path != null)
        {
            for (int i = 0; i < path.Count; i++)
            {
                path[i].transform.GetComponent<Renderer>().material = RefHolder.instance.worldGen.m2;
            }
            MainClass.StartMoveToCorotine(path,xIndex,yIndex);
            
        }
        else
        {
            Debug.LogError("Invalid Path");
        }
    }

    public IEnumerator MoveToTileEnumerator(List<GameObject> path,int xIndex,int yIndex)
    {
        float step = speed * Time.deltaTime;
        Vector3 target;
        for (int i = 1; i < path.Count; i++)
        {
            target = path[i].transform.position + new Vector3(0f,0.5f,0f);
            
            while (MainClass.transform.position != target)
            {
                MainClass.transform.position = Vector3.MoveTowards(MainClass.transform.position, target, step);
                yield return new WaitForFixedUpdate();
                //Debug.LogError("hola from inside " + target);
            }
            
        }
        for (int i = 0; i < path.Count; i++)
        {
            path[i].transform.GetComponent<Renderer>().material = RefHolder.instance.worldGen.m1;
        }
        MainClass.SetMapIndex(xIndex, yIndex);


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
