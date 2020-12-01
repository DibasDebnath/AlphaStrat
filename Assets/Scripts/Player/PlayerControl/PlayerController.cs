﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GroundTap()
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

            }

           

            // Do something with the object that was hit by the raycast.
        }
    }
}