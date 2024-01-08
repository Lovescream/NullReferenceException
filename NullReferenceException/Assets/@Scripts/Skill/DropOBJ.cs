using System.Collections;
using UnityEngine;

public class DropOBJ : MonoBehaviour
{
    public GameObject[] dropOBJ;
    public int[] dropChance;
    System.Random random = new System.Random();

    public void MobDrop()
    {
        int randomNumber = random.Next(1, 101);

        for (int i = 0; i < dropOBJ.Length; i++)
        {
            if (randomNumber <= dropChance[i])
            {
                Instantiate(dropOBJ[i], transform.position, Quaternion.identity);
                break;
            }
        }

        Debug.Log("²Î");
    }
}
