using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Damage");
        }
    }
}
