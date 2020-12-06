using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera cam;



    [Header("Controller Varriables")]
    public bool isHeroSelected;
    public HeroMainBase selectedHero;



    //Test Variables
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
                SelectGround(hit);   
            }
            else if(hit.transform.CompareTag("Hero"))
            {
                SelectHero(hit);
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
            Debug.Log("Ground Hit index " + gs.xGridIndex + " " + gs.yGridIndex);
            int x = gs.xGridIndex;
            int y = gs.yGridIndex;
            if (selectedHero.heroSO.IsGroundHighlighted(x , y))
            {
                selectedHero.heroSO.MoveToSelectedTile(x, y);
            }
            selectedHero.OnDeselect();
        }
    }

    public void SelectEnemy()
    {

    }
   
}
