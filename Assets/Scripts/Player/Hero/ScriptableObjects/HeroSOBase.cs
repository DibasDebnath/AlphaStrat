using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroSOBase : ScriptableObject
{
    public string heroName;
    public string heroType;

    [Range(0.0f, 100.0f)]
    public float health;
    [Range(0.0f, 100.0f)]
    public float attack;
    [Range(0.0f, 100.0f)]
    public float defence;
    [Range(0.0f, 100.0f)]
    public float heal;
    [Range(0f, 10f)]
    public int range;

    public int speed;

    

}
