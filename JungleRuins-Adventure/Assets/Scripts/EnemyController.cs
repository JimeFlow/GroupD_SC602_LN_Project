using System.Collections;
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
    bool isAggroType;

    [SerializeField]
    bool isPatrolType;

    [SerializeField]
    GameObject attackRange;

    [SerializeField]
    GameObject Hit;

    [SerializeField]
    float attackCooldown = 1.0f; // Cooldown time between attacks

    Rigidbody2D _rigidbody;
    Animator _animator;

    private Vector2 _originalPosition;
    private bool isDead = false;
    public bool isAttacking;

    private AggroController _aggroController;
    private PatrolController _patrolController;

    private float _nextAttackTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        ANIMATION_DIE = Animator.StringToHash("die");
        ANIMATION_SPEED = Animator.StringToHash("speed");
        ANIMATION_ATTACK = Animator.StringToHash("attack");

        _aggroController = GetComponent<AggroController>();
        _patrolController = GetComponent<PatrolController>();
    }

    private void Start()
    {
        _originalPosition = transform.position;
    }

    public void Update()
    {
        if (isDead) return;

        if (isAttacking) return;
        
        if (!isAttacking)
        {
            ColliderWeaponFalse();
        }

        if (isAggroType)
        {
            _aggroController.CheckAggro();
        }

        _animator.SetFloat(ANIMATION_SPEED, Mathf.Abs(_rigidbody.velocity.x));
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        if (isAttacking) return;

        if (isAggroType)
        {
            _aggroController.GroundCheckWhileAggro();
        }
    }

    public void StartAttack()
    {
        if (Time.time >= _nextAttackTime)
        {
            _nextAttackTime = Time.time + attackCooldown;
            _animator.SetTrigger(ANIMATION_ATTACK);
            isAttacking = true;
            ColliderWeaponTrue();
        }
    }

    public void Die()
    {
        //SoundManager.Instance.PlaySFX(dieSoundSFX);
        if (isDead) return;
        isDead = true;
        StartCoroutine(DieCoroutine());
    }
   
    private IEnumerator DieCoroutine()
    {
        _animator.SetTrigger(ANIMATION_DIE);
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

    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ResetAttackState()
    {   
        _animator.SetBool(ANIMATION_ATTACK, false);
        isAttacking = false;
        attackRange.GetComponent<BoxCollider2D>().enabled = true;
    }

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
