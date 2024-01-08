using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    [SerializeField] private float hungerIncreaseTime = 10f; // 배고픔 감소 시간
    private bool isPlayer = false; // 플레이어가 근처에 있는지

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = true;
            StartCoroutine(CoIncreaseHunger(other));
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = false;
            StopCoroutine(CoIncreaseHunger(other));
        }
    }
    IEnumerator CoIncreaseHunger(Collider2D collider2D)
    {
        while (isPlayer)
        {
            yield return new WaitForSeconds(hungerIncreaseTime);
            Debug.Log("플레이어의 배고픔과 체력이 증가합니다.");
            var player = collider2D.GetComponent<Player>();
            player.Hp += 10;
            player.Hunger += 2;
        }
    }
}
