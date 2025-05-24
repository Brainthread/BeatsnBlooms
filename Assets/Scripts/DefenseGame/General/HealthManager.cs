using UnityEngine;
using System;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 6;
    private float health;
    [SerializeField] private UnityEvent OnTakeDamage;
    private bool canTakeDamage = true;

    void Start()
    {
        health = maxHealth;
    }

    public void ApplyDamage(float damage, bool overrideInvul)
    {
        if(canTakeDamage||overrideInvul) {
            OnTakeDamage.Invoke();
            health -= damage;
            if (health <= 0)
            {
                HealthDepleted();
            }
        }

    }

    public void ApplyHealing(float healing)
    {
        health += healing;
        if(health>maxHealth)
            health = maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        ApplyDamage(damage, false);
    }

    public Action onHealthDepleted;
    public void HealthDepleted()
    {
        if(onHealthDepleted != null)
        {
            onHealthDepleted();
        }
    }

    private void OnDestroy()
    {
        onHealthDepleted = null;
    }

    public void SetInvulnerability (bool canTakeDamage)
    {
        this.canTakeDamage = canTakeDamage;
    }
}
