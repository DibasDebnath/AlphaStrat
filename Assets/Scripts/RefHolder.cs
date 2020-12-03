using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefHolder : MonoBehaviour
{
    public static RefHolder instance;


    
    public PlayerController playerController;
    public InputController inputController;
    public WorldGen worldGen;
    public PathFinding pathFinding;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
