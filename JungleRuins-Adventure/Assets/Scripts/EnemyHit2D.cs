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
            CharacterController2D controller = collision.GetComponent<CharacterController2D>();
            if (controller != null)
            {
                EnemyController enemy = GetComponentInParent<EnemyController>();
                if (enemy != null)
                {
                    if (enemy.ColliderWeaponTrue() == true)
                    {
                        controller.Die();
                    }
                }

            }
        }
    }
}
