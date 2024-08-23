using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableController : MonoBehaviour
{
    [SerializeField]
    float maxHealth;

    [SerializeField]
    GameObject itemToSpawn;

    [SerializeField]
    string breakBarrelSFX;

    [SerializeField]
    string breakCrateSFX;

    [SerializeField]
    bool isEnemy;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float value, bool isPercentage = false)
    {
        float damage = Mathf.Abs(value);

        if (isPercentage)
        {
            damage = maxHealth * 100 / maxHealth;
        }

        _currentHealth -= damage;

        if (gameObject.CompareTag("Crate"))
        {
            if (!string.IsNullOrEmpty(breakCrateSFX))
            {
                SoundManager.Instance.PlaySFX(breakCrateSFX);
            }
        }


        if (_currentHealth <= 0)
        {
            if (gameObject.CompareTag("Crate"))
            {
                if (itemToSpawn != null)
                {
                    SpawnItem();
                }
            }
            else if (gameObject.CompareTag("Barrel"))
            {
                if (!string.IsNullOrEmpty(breakBarrelSFX))
                {
                    SoundManager.Instance.PlaySFX(breakBarrelSFX);
                }
            }
            else if (isEnemy)
            {
                EnemyController enemy = GetComponent<EnemyController>();

                if(enemy != null)
                {
                    enemy.Die();
                }
            }

            Destroy(gameObject);
        }
    }

    private void SpawnItem()
    {
        if (itemToSpawn != null)
        {
            itemToSpawn.SetActive(true);
        }
    }

}
