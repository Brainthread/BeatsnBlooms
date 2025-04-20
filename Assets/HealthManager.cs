using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 6;
    private float health;
    [SerializeField] private AudioClip damageClip;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        GetComponent<AudioSource>().PlayOneShot(damageClip, 1.5f);
        health -= damage;
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
