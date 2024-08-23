using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillboxController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterController2D controller = collision.GetComponent<CharacterController2D>();
            if (controller != null)
            {
                controller.Die();
            }
        }
    }
}
