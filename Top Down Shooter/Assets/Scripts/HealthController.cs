using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    // why we use SerializeField: https://docs.unity3d.com/ScriptReference/SerializeField.html
    // (basically because we use private)
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;
    

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public bool IsInvincivble { get; set; }

    public UnityEvent OnDied;

    public UnityEvent OnDamaged;

    public UnityEvent OnHealthChanged;

    public void TakeDamage(float damageAmount)
    {
        if (IsInvincivble)
        {
            return;
        }


        if (_currentHealth == 0)
        {
            OnDied.Invoke();
        }

        _currentHealth -= damageAmount;

        OnHealthChanged.Invoke();

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        else
        {
            OnDamaged.Invoke();
        }
    }
}
