using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    [SerializeField] private float hungerIncreaseTime = 10f; // ����� ���� �ð�
    private bool isPlayer = false; // �÷��̾ ��ó�� �ִ���

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
            Debug.Log("�÷��̾��� ����İ� ü���� �����մϴ�.");
            var player = collider2D.GetComponent<Player>();
            player.Hp += 10;
            player.Hunger += 2;
        }
    }
}
