using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    private int ANIMATION_SPEED;
    private int ANIMATION_FORCE;
    private int ANIMATION_FALL;
    private int ANIMATION_SLASH;
    private int ANIMATION_DIE;

    [Header("Movement")]
    [SerializeField]
    float walkSpeed;

    public float MoveSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }

    [SerializeField]
    float jumpForce;

    public float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }

    [SerializeField]
    float gravityMultiplier;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Vector2 groundCheckSize;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    bool isFacingRight;

    [Header("Attack")]
    [SerializeField]
    Transform slashPoint;

    [SerializeField]
    float slashRadius;

    [SerializeField]
    LayerMask attackMask;

    [SerializeField]
    string slashSoundSFX;

    [SerializeField]
    float dieDelay;

    [Header("Sound Effects")]
    [SerializeField]
    private string jumpSoundSFX;

    Rigidbody2D _rigibody;
    Animator _animator;

    public CoinManager _coinManager;

    float _inputX;
    float _gravityY;
    float _velocityY;

    bool _isGrounded;
    bool _isJumpPressed;
    bool _isJumping;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        _gravityY = Physics2D.gravity.y;

        ANIMATION_SPEED = Animator.StringToHash("speed");
        ANIMATION_FORCE = Animator.StringToHash("force");
        ANIMATION_FALL = Animator.StringToHash("fall");
        ANIMATION_SLASH = Animator.StringToHash("slash");
        ANIMATION_DIE = Animator.StringToHash("die");
    }

    private void Start()
    {
        HandleGrounded();
    }

    private void Update()
    {
        HandleGravity();
        HandleInputMove();
    }

    private void FixedUpdate()
    {
        HandleJump();
        HandleRotate();
        HandleMove();
    }


    private void HandleGrounded()
    {
        _isGrounded = IsGrounded();
        if (!_isGrounded)
        {
            StartCoroutine(WaitForGroundedCoroutine());
        }
    }

    private void HandleGravity()
    {
        if (_isGrounded)
        {
            if (_velocityY < -1.0F)
            {
                _velocityY = -1.0F;
            }

            HandleInputJump();
        }
    }

    private void HandleInputJump()
    {
        _isJumpPressed = Input.GetButton("Jump");
    }

    private void HandleInputMove()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
    }

    private void HandleJump()
    {
        if (_isJumpPressed)
        {
            _isJumpPressed = false;
            _isGrounded = false;
            _isJumping = true;

            _velocityY = jumpForce;

            _animator.SetTrigger(ANIMATION_FORCE);

            SoundManager.Instance.PlaySFX(jumpSoundSFX);

            StartCoroutine(WaitForGroundedCoroutine());
        }
        else if (!_isGrounded)
        {
            _velocityY += _gravityY * gravityMultiplier * Time.fixedDeltaTime;
            if (!_isJumping)
            {
                _animator.SetTrigger(ANIMATION_FALL);
            }
        }
        else if (_isGrounded)
        {
            if (_velocityY >= 0.0F)
            {
                _velocityY = -1.0F;
            }
            else
            {
                HandleGrounded();
            }

            _isJumping = false;
        }
    }

    private void HandleMove()
    {
        float speed = _inputX != 0.0F ? 1.0F : 0.0f;
        float animatorSpeed = _animator.GetFloat(ANIMATION_SPEED);

        if (speed != animatorSpeed)
        {
            _animator.SetFloat(ANIMATION_SPEED, speed);
        }

        Vector2 velocity = new Vector2(_inputX, 0.0F) * walkSpeed * Time.fixedDeltaTime;
        velocity.y = _velocityY;

        _rigibody.velocity = velocity;
    }

    private void HandleRotate()
    {
        if (_inputX == 0.0F)
        {
            return;
        }

        bool facingRight = _inputX > 0.0F;
        if (isFacingRight != facingRight)
        {
            isFacingRight = facingRight;
            transform.Rotate(0.0F, 180.0F, 0.0F);
        }
    }

    private bool IsGrounded()
    {
        Collider2D collider2D =
            Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0.0F, groundMask);
        return collider2D != null;
    }

    private IEnumerator WaitForGroundedCoroutine()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(() => IsGrounded());
        _isGrounded = true;
    }

    public void Slash()
    {
        _animator.SetTrigger(ANIMATION_SLASH);
        SoundManager.Instance.PlaySFX(slashSoundSFX);
    }

    public void Slash(float damage, bool isPercentage)
    {
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(slashPoint.position, slashRadius, attackMask);

        foreach (Collider2D collider in colliders)
        {
            DamageableController controller = collider.GetComponent<DamageableController>();
            if (controller == null)
            {
                continue;
            }

            controller.TakeDamage(damage, isPercentage);
        }
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        _animator.SetTrigger(ANIMATION_DIE);
        yield return new WaitForSeconds(dieDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmos() //Dibuja un cuadrado rojo donde esta el Ground Check
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            _coinManager.coinCount++;
        }
    }
}