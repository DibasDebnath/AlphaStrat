using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMainBase : MonoBehaviour
{

    public HeroSOBase heroSO;

    
    
    public int xGridIndex;
    public int yGridIndex;

    

    private List<GameObject> highlightedGrounds;



    
    public List<GameObject> HighlightedGrounds { get => highlightedGrounds; set => highlightedGrounds = value; }


    private void Awake()
    {
        HighlightedGrounds = null;
    }

    private void Start()
    {
       
    }

    public void OnSelect()
    {

        HighlightMovableTiles();


    }

    public void OnDeselect()
    {

        DeHighlightMovableTiles();
    }



    public void SetMapIndex(int xGridIndex,int yGridIndex)
    {
        this.xGridIndex = xGridIndex;
        this.yGridIndex = yGridIndex;
    }

    









   

    

    #region Main Turn Methodes 

    

    public void HighlightMovableTiles()
    {
        Debug.Log(xGridIndex + yGridIndex);
        HighlightedGrounds = RefHolder.instance.pathFinding.GetPathObjectsInRange(xGridIndex, yGridIndex, heroSO.range);

        for (int i = 0; i < HighlightedGrounds.Count; i++)
        {
            HighlightedGrounds[i].transform.GetComponent<GroundScript>().GlowOneEnable();
        }
    }

    public void DeHighlightMovableTiles()
    {
        Debug.Log(xGridIndex + yGridIndex);
        HighlightedGrounds = RefHolder.instance.pathFinding.GetPathObjectsInRange(xGridIndex, yGridIndex, heroSO.range);

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

    public void MoveToSelectedTile(int xIndex, int yIndex)
    {
        List<GameObject> path = RefHolder.instance.pathFinding.GetPathObjects(xGridIndex, yGridIndex, xIndex, yIndex);
        if (path != null)
        {
            for (int i = 0; i < path.Count; i++)
            {
                path[i].transform.GetComponent<Renderer>().material = RefHolder.instance.worldGen.m2;
            }
            RefHolder.instance.worldGen.GetGridGroundObject(xGridIndex, yGridIndex).GetComponent<GroundScript>().SetGridEmpty();
            StartCoroutine(MoveToTileEnumerator(path, xIndex, yIndex));

        }
        else
        {
            Debug.LogError("Invalid Path");
        }
    }

    public IEnumerator MoveToTileEnumerator(List<GameObject> path, int xIndex, int yIndex)
    {
        float step = heroSO.speed * Time.deltaTime;
        Vector3 target;
        for (int i = 1; i < path.Count; i++)
        {
            target = path[i].transform.position + new Vector3(0f, 0.5f, 0f);

            while (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, step);
                yield return new WaitForFixedUpdate();
                //Debug.LogError("hola from inside " + target);
            }

        }
        for (int i = 0; i < path.Count; i++)
        {
            path[i].transform.GetComponent<Renderer>().material = RefHolder.instance.worldGen.m1;
        }
        SetMapIndex(xIndex, yIndex);
        RefHolder.instance.worldGen.GetGridGroundObject(xIndex, yIndex).GetComponent<GroundScript>().SetGridNotEmpty();


    }
    #endregion



    #region Support Methods

    public bool IsGroundHighlighted(int xGridIndex, int yGridIndex)
    {
        bool highlighted = false;
        if (highlightedGrounds == null)
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
