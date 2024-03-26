using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEALTH : MonoBehaviour
{
    public delegate void HitEvent(GameObject source);
    public HitEvent onHit;

    public delegate void ResetEvent();
    public ResetEvent onHitReset;

    public COOLDOWN invul;

    public float maxHealth = 10F;
    public float currentHealth
    {
        get { return _currentHealth; }
    }

    private float _currentHealth = 10F;
    private bool _canDamage = true;

    void Start()
    {
        ResetHealthToMax();
    }

    void Update()
    {
        ResetInvulnerable();
    }

    public void Damage(float damage, GameObject source)
    {
        if (!_canDamage)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0F)
        {
            _currentHealth = 0F;
            Die();
        }

        invul.StartCooldown();
        _canDamage = true;

        onHit?.Invoke(source);
    }

    void ResetInvulnerable()
    {
        if (_canDamage)
            return;

        if (invul.isOnCooldown && _canDamage == false)
            return;

        _canDamage = true;
        onHitReset?.Invoke();
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void ResetHealthToMax()
    {
        _currentHealth = maxHealth;
    }
}
