using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tank", menuName = "Cards/Hero/Tank")]
public class TankSO : HeroSOBase
{
    



    private TankSO()
    {
        heroType = "Tank";
        health = 80f;
        attack = 40f;
        defence = 70f;
        heal = 30f;
        range = 5;
    }


   
    
}
