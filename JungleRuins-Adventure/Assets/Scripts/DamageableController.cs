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
    string damageEnemySFX;

    [SerializeField]
    bool isEnemy;

    private float _currentHealth;
    public LevelManager LevelManager;

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

        if (gameObject.CompareTag("Enemy"))
        {
            if (!string.IsNullOrEmpty(damageEnemySFX))
            {
                SoundManager.Instance.PlaySFX(damageEnemySFX);
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
                Debug.Log("HIT ENEMY");
                EnemyController enemy = GetComponent<EnemyController>();

                if(enemy != null)
                {
                    enemy.Die();
                    SpawnItem();
                }
            }

            Destroy(gameObject);
            if (gameObject.name == "Boss")
            {
                Debug.Log("BOSS DEAD");                            
                LevelManager.GameOverLevel();
            }
        }
    }

    private void SpawnItem()
    {
        if (itemToSpawn != null)
        {
            //itemToSpawn.SetActive(true);
            Vector3 spawnPosition = transform.position + new Vector3(0, 13.0f, 0);
            Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
        }
    }

}
