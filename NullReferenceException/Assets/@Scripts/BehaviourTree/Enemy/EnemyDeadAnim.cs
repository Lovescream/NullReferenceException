using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadAnim : MonoBehaviour
{
    public void DeadEnemy()
    {
        gameObject.SetActive(false);
    }
}
