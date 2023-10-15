using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCheck : MonoBehaviour
{
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;

    BoxCollider2D boxCollider2D;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(check(enemy1) && check(enemy2))
        {
            // Disable BoxCollider2D
            boxCollider2D.enabled = false;
        }
    }

    private bool check(GameObject enemy)
    {
        return enemy == null || !enemy.activeInHierarchy;
    }

    
}
