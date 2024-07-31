using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterListener : MonoBehaviour
{
    [SerializeField]
    float damage;

    [SerializeField]
    bool isPercentage;

    private CharacterController2D _character;

    private void Awake()
    {
        _character = GetComponentInParent<CharacterController2D>();
    }

    public void OnSlash()
    {
        _character.Slash(damage, isPercentage);
    }
}
