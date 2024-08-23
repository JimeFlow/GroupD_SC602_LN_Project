using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : MonoBehaviour
{
    private int ANIMATION_SPEED;
    private int ANIMATION_DIE;
    private int ANIMATION_ATTACK;

    public Animator _animator;
    public EnemyController enemy;


    void Start()
    {
        ANIMATION_DIE = Animator.StringToHash("die");
        ANIMATION_SPEED = Animator.StringToHash("speed");
        ANIMATION_ATTACK = Animator.StringToHash("attack");
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetFloat(ANIMATION_SPEED, 0);
            _animator.SetBool(ANIMATION_ATTACK, true);
            enemy.isAttacking = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
