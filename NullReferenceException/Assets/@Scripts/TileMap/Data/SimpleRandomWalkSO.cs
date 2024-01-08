using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParmeter_",menuName = "PCG/ SimpleRandomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iteration = 10, walkLangth = 10;
    public bool startRandomlyEachIteration = true;
}
