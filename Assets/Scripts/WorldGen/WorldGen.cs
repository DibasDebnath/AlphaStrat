using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{

    #region Instance

    public static WorldGen instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion


    #region MonoFunctions

    private void Start()
    {
        GridGenaration();
    }

    #endregion

    #region GridComponent


    [Header("Gounds")]
    public Transform gridHolder;
    public List<GameObject> groundTileList = new List<GameObject>();



    [Header("Map Perameters")]
    public int xTileCount;
    public int zTileCount;
    public int bounderySize;
    public int bounderySizeForPlayerSpawn;
    //public bool groundBool;
    public int totalTileCount;
    public int totalOccupiedTileCount;
    public Transform mapStartTramsformPosition;
    public float xRightIncreaseValue; // How Much to the right generatin moves (Usually Grid Cell Size devided by 2)
    public float zRightIncreaseValue; // How Much to the right generatin moves (Usually Grid Cell Size devided by 2)
    public float xDownIncreaseValue; // How Much to the right generatin moves (Usually Grid Cell Size devided by 2)
    public float zDownIncreaseValue; // How much down generation moves (Usually Grid Cell Size devided by 2)

    [Header("Grid Variables")]

    public GameObject[,] gridGroundReference;





    void GridGenaration()
    {
        Vector3 startPos = mapStartTramsformPosition.position;


        //Get Data From Mission
        //xTileCount = missionDataScript.missions[missionNumberToGenerate].difficulties[difficulty].xMapSize;
        //yTileCount = missionDataScript.missions[missionNumberToGenerate].difficulties[difficulty].yMapSize;



        gridGroundReference = new GameObject[xTileCount, zTileCount];

        //startPos.Set(xStartPos, yStartPos, 0);
        totalTileCount = 0;
        Vector3 previousLeftStartPosition = startPos;
        Vector3 tmpPos = startPos;
        GameObject tmpGround;



        for (int i = 0; i < xTileCount; i++)
        {
            for (int j = 0; j < zTileCount; j++)
            {
                //Creating Ground Tile for the Grid
                //if (groundBool)
                //{
                gridGroundReference[i, j] = tmpGround = Instantiate(GetRandomGroundTile(), tmpPos, Quaternion.identity, gridHolder);
                
                //}

                //Setting Grid Data Array and Adding in
                tmpGround.GetComponent<GroundScript>().SetGridData(i, j, tmpPos);
                tmpGround.SetActive(true);

                //tmpGround.GetComponent<SpriteRenderer>().sortingOrder = tmpOrderInLayer;
                totalTileCount++;
                tmpPos.x += xRightIncreaseValue;
                tmpPos.z += zRightIncreaseValue;
                
                //Debug.LogWarning(gridDataArray[i, j].xPosition);
            }

            tmpPos.x = previousLeftStartPosition.x + xDownIncreaseValue * (i + 1);
            tmpPos.z = previousLeftStartPosition.z - zDownIncreaseValue * (i + 1);
        }
        //SetTotalTileCountAfterColliderDeactivate();
        //GridColliderDeactivate();
    }

    GameObject GetRandomGroundTile()
    {
        return groundTileList[UnityEngine.Random.Range(0, groundTileList.Count)];
    }

    public GameObject getGridGroundObject(int i, int j)
    {
        return gridGroundReference[i, j];
    }
    //private void SetTotalTileCountAfterColliderDeactivate()
    //{
    //    int tmpcount = 0;
    //    bool[,] tmpArray = new bool[xTileCount, zTileCount];
    //    //Debug.LogError(tmpArray[5,5]);
    //    for (int i = bounderySize; i < xTileCount - bounderySize; i++)
    //    {
    //        for (int j = bounderySize; j < zTileCount - bounderySize; j++)
    //        {
    //            if (tmpArray[i, j] == false)
    //            {
    //                tmpArray[i, j] = true;
    //                tmpcount++;
    //            }

    //        }
    //    }
    //    tmpcount -= (bounderySizeForPlayerSpawn - bounderySize) ^ 2;
    //    totalTileCount = tmpcount;
    //}



    #endregion


}
