using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 6;
    private float health;
    [SerializeField] private AudioClip damageClip;
    private bool canTakeDamage = true;
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private GameObject onDestroySpawn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if(canTakeDamage) {
            GetComponent<AudioSource>().PlayOneShot(damageClip, 1.5f);
            health -= damage;
            if (health < 0)
            {
                if(onDestroySpawn)
                {
                    Instantiate(onDestroySpawn, transform.position, transform.rotation);
                }
                if(destroyOnDeath)
                {
                    Destroy(gameObject);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

    }

    public void SetInvulnerability (bool canTakeDamage)
    {
        this.canTakeDamage = canTakeDamage;
    }
}
