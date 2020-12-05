using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera cam;


    public Material m1;
    public Material m2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tap()
    {
        TapRayCast();
    }

    public void TapRayCast()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(RefHolder.instance.inputController.ScreenTouchPosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                

                GroundScript gs = hit.transform.GetComponent<GroundScript>();
                Debug.Log("Ground Hit index "+gs.xGridIndex+" "+gs.yGridIndex);

                // Do something with the object that was hit by the raycast.
                int x = hit.transform.gameObject.transform.GetComponent<GroundScript>().xGridIndex;
                int y = hit.transform.gameObject.transform.GetComponent<GroundScript>().yGridIndex;

                int xCount = RefHolder.instance.worldGen.xTileCount;
                int zCount = RefHolder.instance.worldGen.zTileCount;

                List<GameObject> path =  RefHolder.instance.pathFinding.GetPathObjects(0,0,x,y,xCount,zCount);
                if(path != null)
                {
                    for (int i = 0; i < path.Count; i++)
                    {
                        path[i].transform.GetComponent<Renderer>().material = RefHolder.instance.worldGen.m1;
                    }
                }
                else
                {
                    Debug.LogError("Invalid Path");
                }
                

            }
            else if(hit.transform.CompareTag("Hero"))
            {
                hit.transform.GetComponent<HeroMainBase>().OnSelect();
            }

           

            
        }
    }
}
