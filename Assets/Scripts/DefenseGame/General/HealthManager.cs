using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 6;
    private float health;
    [SerializeField] private AudioClip damageClip;
    private bool canTakeDamage = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    public void ApplyDamage(float damage, bool overrideInvul)
    {
        if(canTakeDamage||overrideInvul) {
            GetComponent<AudioSource>().PlayOneShot(damageClip, 1.5f);
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
