using UnityEngine;

public class AggroController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float aggroRange;

    [SerializeField]
    private float attackRange; // Range within which the enemy attacks

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask groundMask;

    private Rigidbody2D _rigidbody;
    private EnemyController _enemyController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyController = GetComponent<EnemyController>();
    }

    public bool ShouldAggroPlayer()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        return distToPlayer < aggroRange;
    }

    public bool ShouldAttackPlayer()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        return distToPlayer < attackRange;
    }

    public void AggroPlayer()
    {
        //Debug.Log("AggroPlayer called");
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1F, groundMask);

        if (isGrounded)
        {
            if (transform.position.x < player.position.x)
            {
                //Debug.Log("derecha");
                _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
                transform.localScale = new Vector2(1, 1); 
            }
            else if (transform.position.x > player.position.x) 
            {
                //Debug.Log("izquierda");
                _rigidbody.velocity = new Vector2(-speed, _rigidbody.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {
            //Debug.Log("nada");
            _rigidbody.velocity = Vector2.zero;
        }
    }

    public void StopAggroPlayer()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public void GroundCheckWhileAggro()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5F, groundMask);

        if (!raycastHit)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else if (ShouldAggroPlayer())
        {
            AggroPlayer();
        }
    }

    public void CheckAggro()
    {
        
         if (ShouldAggroPlayer())
        {
            AggroPlayer();
        }
        else
        {
            StopAggroPlayer();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.5F);
    }
}
