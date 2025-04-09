using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 6;
    private float health;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
