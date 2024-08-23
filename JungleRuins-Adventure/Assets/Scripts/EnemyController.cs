using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int ANIMATION_SPEED;
    private int ANIMATION_DIE;
    private int ANIMATION_ATTACK;

    [Header("Die")]
    [SerializeField]
    float dieTime;

    [SerializeField]
    string dieSoundSFX;

    [Header("Tipo de Enemigo")]
    [SerializeField]
    bool isAgroType;

    [SerializeField]
    bool isPatrolType;

    [SerializeField]
    GameObject attackRange;

    [SerializeField]
    GameObject Hit;

    Rigidbody2D _rigidbody;
    Animator _animator;

    private Vector2 _originalPosition;
    private bool isDead = false;
    public bool isAttacking;

    private AgroController _agroController;
    private PatrolController _patrolController;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        ANIMATION_DIE = Animator.StringToHash("die");
        ANIMATION_SPEED = Animator.StringToHash("speed");
        ANIMATION_ATTACK = Animator.StringToHash("attack");

        _agroController = GetComponent<AgroController>();
        _patrolController = GetComponent<PatrolController>();
        
    }

    private void Start()
    {
        _originalPosition = transform.position;
    }

    
    private void Update()
    {
        if(isDead) return;

        if(isAgroType)
        {
            _agroController.CheckAgro();
        }

        _animator.SetFloat(ANIMATION_SPEED, Mathf.Abs(_rigidbody.velocity.x));
        


    }

    private void FixedUpdate()
    {
        if(isDead) return;

        if (isAgroType)
        {
            _agroController.GroundCheckWhileAgro();
        }
        if (isPatrolType)
        {
            _patrolController.PatrolGroundCheck();
        }
        
    }

    public void Die()
    {
        SoundManager.Instance.PlaySFX(dieSoundSFX);
        if(isDead) return; 
        isDead = true;
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        //_animator.SetTrigger(ANIMATION_DIE);
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }

    public void FinalAttackAnimation()
    {
        _animator.SetBool(ANIMATION_ATTACK, false);
        isAttacking = false;
        attackRange.GetComponent<BoxCollider2D>().enabled = true;

    }

    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;

    }

    public void ColliderWeaponeFalse()
    {
        Hit.GetComponent <BoxCollider2D>().enabled = false;
    }
}
