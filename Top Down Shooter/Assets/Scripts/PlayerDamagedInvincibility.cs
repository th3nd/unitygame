using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedInvincibility : MonoBehaviour
{
    [SerializeField]
    private float _invincibilityDuration;
    private InvincibilityController _invincibilityController;

    public Animator animator;

    private void Awake()
    {
        _invincibilityController = GetComponent<InvincibilityController>();
    }

    public void StartInvincibility()
    {
        animator.Play("ANIM_Player");
        _invincibilityController.StartInvincibility(_invincibilityDuration);
        Invoke("DelayedInvis", _invincibilityDuration);
    }

    private void DelayedInvis()
    {
        animator.Play("Default");
    }
}
