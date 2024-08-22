using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUpController : MonoBehaviour
{
    [SerializeField]
    int jumpBonus;

    [SerializeField]
    string powerUpSFX1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController2D _character = collision.gameObject.GetComponent<CharacterController2D>();

            if (_character != null)
            {
                _character.JumpForce += jumpBonus;
                SoundManager.Instance.PlaySFX(powerUpSFX1);

                Destroy(gameObject);
            }
        }
    }
}
