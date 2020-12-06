using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera cam;



    [Header("Controller Varriables")]
    public bool isHeroSelected;
    public HeroMainBase selectedHero;



   

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

        for(int i = 0;i < 5; i++)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Hero"))
                {
                    SelectHero(hit);
                    Debug.Log("Hero");
                    break;
                }
                else if (hit.transform.CompareTag("Ground"))
                {
                    SelectGround(hit);
                    Debug.Log("Ground");
                    break;
                }
            }
        }
        
        
            

        
    }



    public void SelectHero(RaycastHit hit)
    {
        if (!isHeroSelected)
        {
            isHeroSelected = true;
            selectedHero = hit.transform.GetComponent<HeroMainBase>();
            selectedHero.OnSelect();
        }
        
    }

    public void SelectGround(RaycastHit hit)
    {
        if (isHeroSelected)
        {
            isHeroSelected = false;
            
            GroundScript gs = hit.transform.GetComponent<GroundScript>();
            //Debug.Log("Ground Hit index " + gs.xGridIndex + " " + gs.yGridIndex);
            int x = gs.xGridIndex;
            int y = gs.yGridIndex;
            if (selectedHero.IsGroundHighlighted(x , y))
            {
                selectedHero.MoveToSelectedTile(x, y);
            }
            selectedHero.OnDeselect();
        }
        else
        {
            if(selectedHero != null)
            {
                selectedHero.OnDeselect();
            }
            
        }
    }

    public void SelectEnemy()
    {

    }
   
}
