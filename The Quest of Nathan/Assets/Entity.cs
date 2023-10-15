using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public int HP;

    public void TakeDamage()
    {
        HP--;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
