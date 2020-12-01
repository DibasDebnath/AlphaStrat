using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{


    InputSystem inputActions;
    [SerializeField]
    private bool showDebug;

    public bool takeInputsBool;
    public float moveThreshold;


    //Private Variables
    bool tap;
    private bool processedTap;
    bool move;
    float moveDisplacement;
    Vector2 screenTouchPosition;
    Vector2 currentTouchPosition;
    Vector2 oldTouchPosition;


    //Public Call to Variables
    public bool Move { get => move; set => move = value; }
    public float MoveDisplacement { get => moveDisplacement; set => moveDisplacement = value; }
    public bool ProcessedTap { get => processedTap; set => processedTap = value; }
    public Vector2 ScreenTouchPosition { get => screenTouchPosition; set => screenTouchPosition = value; }



    #region Mono Functions

    private void Awake()
    {

        inputActions = new InputSystem();
        inputActions.Player.TouchStart.performed += ctx => tap = true;
        inputActions.Player.TouchEnd.performed += ctx => tap = false;
        inputActions.Player.TouchMove.performed += ctx => screenTouchPosition = ctx.ReadValue<Vector2>();
        inputActions.Player.TouchTap.performed += ctx =>  Tap();

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (takeInputsBool)
        {
            processedTap = tap;
            TakeInputs();
            if (showDebug)
            {
                Debug.Log("Moving at the speed of "+moveDisplacement);
            }
        }
        else if (processedTap == true || move == true)
        {
            processedTap = false;
            move = false;
        }


        
    }


    #endregion



    #region Input Functions

    void TakeInputs()
    {
        if (processedTap)
        {
            //Debug.LogError("takes input");
            currentTouchPosition.x = screenTouchPosition.x / Screen.width;
            currentTouchPosition.y = screenTouchPosition.y / Screen.height;

            if (oldTouchPosition.x != currentTouchPosition.x && oldTouchPosition.x != 0 && Math.Abs(oldTouchPosition.x - currentTouchPosition.x) > moveThreshold)
            {
                moveDisplacement = oldTouchPosition.x - currentTouchPosition.x;
                move = true;
            }
            else
            {
                moveDisplacement = 0;
                move = false;
            }

            oldTouchPosition.x = currentTouchPosition.x;
            oldTouchPosition.y = currentTouchPosition.y;
        }
        else
        {
            currentTouchPosition.x = 0;
            currentTouchPosition.y = 0;
            oldTouchPosition.x = 0;
            oldTouchPosition.y = 0;
            moveDisplacement = 0;
            move = false;
        }
    }


    private void Tap()
    {
        
        if (showDebug)
        {
            Debug.Log("Tapped "+screenTouchPosition.x+" "+screenTouchPosition.y);
        }

        RefHolder.instance.playerController.GroundTap();
    }


    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    #endregion




}
