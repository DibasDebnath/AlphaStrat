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
        // New Gen Static
        //GridSetupFirst();
        GridSetupAfter();

        // New Gen Dynamic
        //GridGenaration();
        //setGridGroundList();

        //Old Gen Use
        //SetGridGroundReferenceFromList();

        //List<HeroMainBase> heroMainBases = GetAllHeroScripts();
        //testHeroClass.Test();
    }

    #endregion

    #region GridComponent

    [Header("Test Variables")]

    public Material m1;
    public Material m2;
    public Material m3;

    public HeroMainBase testHeroClass;


    [Header("For Set Up")]
    public Transform gridHolderForSet;

    [Header("Gounds")]
    public Transform gridHolder;
    public List<GameObject> groundTileList = new List<GameObject>();

    [Header("Heros")]
    public GameObject HeroHolder;

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

    public static GameObject[,] gridGroundReference;
    public List<GameObject> gridGroundReferenceList = new List<GameObject>();
    private void SetGridGroundList()
    {
        for (int i = 0; i < xTileCount; i++)
        {
            for (int j = 0; j < zTileCount; j++)
            {
                gridGroundReferenceList.Add(GetGridGroundObject(i, j));
            }
        }
    }
    private void SetGridGroundReferenceFromList() 
    {
        gridGroundReference = new GameObject[xTileCount, zTileCount];
        for (int i = 0;i<gridGroundReferenceList.Count;i++)
        {
            gridGroundReference[gridGroundReferenceList[i].transform.GetComponent<GroundScript>().xGridIndex, gridGroundReferenceList[i].transform.GetComponent<GroundScript>().yGridIndex] = gridGroundReferenceList[i];
        }
        
    }



    




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
                tmpGround.GetComponent<GroundScript>().SetGridData(i, j);



                //Test Codes for mat change
                if(Random.Range(0,15) == 10)
                {
                    tmpGround.GetComponent<GroundScript>().isEmpty = false; ;
                    tmpGround.transform.GetComponent<Renderer>().material = m2;
                }

                


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

    public GameObject GetGridGroundObject(int i, int j)
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


    #region New SetUp

    public void GridSetupFirst()
    {
        gridGroundReference = new GameObject[xTileCount, zTileCount];

        GameObject XAxis = gridHolderForSet.transform.GetChild(0).gameObject;

        GameObject tmpXAxis = XAxis;

        Vector3 tmpPos = XAxis.transform.position;
        GameObject tmpGround;

        for (int j = 0; j < zTileCount; j++)
        {
            gridGroundReference[0, j] = tmpGround = Instantiate(GetRandomGroundTile(), tmpPos, Quaternion.identity, tmpXAxis.transform);
            //Setting Grid Data Array and Adding in
            tmpGround.GetComponent<GroundScript>().SetGridData(0, j);

            tmpGround.SetActive(true);
            totalTileCount++;
            tmpPos.x += xRightIncreaseValue;
            tmpPos.z += zRightIncreaseValue;
        }


        for (int i = 1; i < xTileCount; i++)
        {
            
            tmpPos.x = XAxis.transform.position.x + xDownIncreaseValue * (i );
            tmpPos.z = XAxis.transform.position.z - zDownIncreaseValue * (i );

            tmpXAxis = Instantiate(XAxis, tmpPos, Quaternion.identity, gridHolderForSet.transform);

            

            for (int j = 0; j < zTileCount; j++)
            {
                tmpXAxis.transform.GetChild(j).GetComponent<GroundScript>().SetGridData(i, j);
            }
            
        }
         
    }




    public void GridSetupAfter()
    {
        gridGroundReference = new GameObject[xTileCount, zTileCount];

       


        for (int i = 0; i < xTileCount; i++)
        {
            for (int j = 0; j < zTileCount; j++)
            {
                gridGroundReference[i,j] = gridHolderForSet.transform.GetChild(i).GetChild(j).gameObject;
                gridGroundReference[i, j].GetComponent<GroundScript>().SetGridIndex(i, j);
            }
        }
    }
    #endregion




    #region Hero Setup



    public List<HeroMainBase> GetAllHeroScripts()
    {
        List<HeroMainBase> heroScripts = new List<HeroMainBase>();
        for (int i = 0; i < HeroHolder.transform.childCount; i++)
        {
            heroScripts.Add(HeroHolder.transform.GetChild(i).GetComponent<HeroMainBase>());
            //heroScripts[i].Test();
        }

        
        return heroScripts;
    }



    
    #endregion

}
