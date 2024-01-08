using System.Collections;
using UnityEngine;

public class SoulAbsorptionArea : MonoBehaviour
{
    public float dmg;
    public float radius;
    private Coroutine damageCoroutine;
    public LayerMask targetLayerMask;

    private void OnEnable()
    {
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            radius = circleCollider.radius;
        }

        damageCoroutine = StartCoroutine(DealDamageRepeatedly());
    }

    private void OnDisable()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
    }

    private IEnumerator DealDamageRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayerMask);

            foreach (Collider2D collider in colliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Hp -= dmg;
                    Debug.Log("데미지 입힘: " + dmg + " / 남은 체력: " + enemy.Hp);
                }
            }
        }
    }
}
