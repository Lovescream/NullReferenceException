using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearNormalEnemy : EnemyBasicBT
{
    protected override void Awake()
    {
        base.Awake();
        this._detectDistance = 4;
        this._attackDistance = 1;
        this._movementSpeed = 3;
    }
}
