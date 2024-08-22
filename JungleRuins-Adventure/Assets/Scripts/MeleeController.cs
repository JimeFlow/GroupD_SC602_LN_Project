using UnityEngine;

public class MeleeController : MonoBehaviour
{
    //[SerializeField, Tooltip("Range interval for attacks")]
    //float attackRange;

    //[SerializeField, Tooltip("Amount of attacks per Range")]
    //int attackRate;

    [SerializeField, Tooltip("Cooldown time between attacks in seconds")]
    float attackCooldown;

    private float _attackTime;
    private float _cooldownTimer;

    CharacterController2D _character;

    private void Awake()
    {
        _character = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        _attackTime -= Time.deltaTime;
        _cooldownTimer -= Time.deltaTime;

        if (_attackTime < 0.0f)
        {
            _attackTime = 0.0f;
        }

        if (_cooldownTimer < 0.0f)
        {
            _cooldownTimer = 0.0f;
        }

        if (_attackTime == 0 && _cooldownTimer == 0)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                _character.Slash();
                _cooldownTimer = attackCooldown;  // Reset the cooldown timer
            }
        }
    }
}
