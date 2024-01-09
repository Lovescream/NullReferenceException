using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : NearEnemyBT
{
    protected override void Awake()
    {
        base.Awake();
        this._detectDistance = 5;
        this._attackDistance = 1;
        this._movementSpeed = 2;
        this._actionDistance = 4;
        this.coolTime = 0f;
        this._originCoolTime = this.coolTime;
    }
}
