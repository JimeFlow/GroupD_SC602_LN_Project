using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableController : MonoBehaviour
{
    private int ANIMATION_HURT;
    private int ANIMATION_DEAD;

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
    public Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        ANIMATION_HURT = Animator.StringToHash("Hurt");
        ANIMATION_DEAD = Animator.StringToHash("Dead");

        _rigidbody = GetComponent<Rigidbody2D>();
    }

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

        if (gameObject.CompareTag("Boss"))
        {
            if (!string.IsNullOrEmpty(damageEnemySFX))
            {
                SoundManager.Instance.PlaySFX(damageEnemySFX);
            }
        }


        if (_currentHealth <= 0)
        {
            if(gameObject.name == "Boss")
            {
                Debug.Log("BOSS DEAD");
             
                if(_rigidbody != null)
                {
                    _rigidbody.velocity = Vector2.zero;
                }
                StartCoroutine(BossDeathSequence());
            }


            if (gameObject.CompareTag("Crate"))
            {
                if (itemToSpawn != null)
                {
                    SpawnItem();
                    Destroy(gameObject);

                }
            }
            else if (gameObject.CompareTag("Barrel"))
            {
                if (!string.IsNullOrEmpty(breakBarrelSFX))
                {
                    SoundManager.Instance.PlaySFX(breakBarrelSFX);
                    Destroy(gameObject);

                }
            }
            else if (isEnemy)
            {
                Debug.Log("HIT ENEMY");
                EnemyController enemy = GetComponent<EnemyController>();

                if(enemy != null)
                {
                    if (_rigidbody != null)
                    {
                        _rigidbody.velocity = Vector2.zero;
                    }
                    enemy.Die();
                    SpawnItem();
                    

                }
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

    private IEnumerator BossDeathSequence()
    {
        Debug.Log("Setting Dead animation");
        
        _animator.SetBool(ANIMATION_DEAD, true);
        yield return new WaitForSeconds(2);

        Debug.Log("Destroying Boss");
        Destroy(gameObject);
        Debug.Log("Calling GameOverLevel");
        LevelManager.GameOverLevel();

    }
}
