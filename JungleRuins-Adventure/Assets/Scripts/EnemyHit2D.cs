using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.activeSelf)
        {
            print("Damage");
            CharacterController2D controller = collision.GetComponent<CharacterController2D>();
            if (controller != null)
            {
                EnemyController enemy = GetComponentInParent<EnemyController>();
                if (enemy != null && enemy.ColliderWeaponTrue())
                {
                    if (enemy.isAttacking)
                    {
                        controller.Die();
                    }
                    
                }

            }
        }
    }
}
