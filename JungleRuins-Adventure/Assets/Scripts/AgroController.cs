using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float agroRange;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask groundMask;

    private Rigidbody2D _rigidbody;

    //private float _atackRange;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    

    public bool ShouldAgroPlayer()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        return distToPlayer < agroRange;
    }

    public void AgroPlayer()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1F, groundMask);

        if (transform.position.x < player.position.x) //si estan a la izquierda del jugador
        {
            _rigidbody.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(1,1); //mira a la izquierda
        }
        else if(transform.position.x > player.position.x)// si estan a la derecha del jugador
        {
            _rigidbody.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(-1, 1); // mira a la derecha
        }
    }

    public void StopAgroPlayer()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public void GroundCheckWhileAgro()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.40F, groundMask);

        if (!raycastHit)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            if (ShouldAgroPlayer())
            {
                Vector2 direction = (player.position - transform.position).normalized;
                _rigidbody.velocity = new Vector2(direction.x * speed, _rigidbody.velocity.y);

                transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
            }
        }
    }

    public void CheckAgro()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        //print("distToPlayer: " + distToPlayer); para revisar distancia

        if (distToPlayer < agroRange)
        {
            AgroPlayer();
        }
        else
        {
            StopAgroPlayer();
            
        }

        
    }
}
