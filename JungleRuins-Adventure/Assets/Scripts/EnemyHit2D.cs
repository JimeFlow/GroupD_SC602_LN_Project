using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit2D : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Daño");
        }
    }
}
