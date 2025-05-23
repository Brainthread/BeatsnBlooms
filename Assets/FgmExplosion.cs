using UnityEngine;

public class FgmExplosion : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float explosiveRange = 40;
    [SerializeField] private bool penetrating = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int enemies = EnemyManager.current.transform.childCount;
        for(int i = enemies-1; i >= 0; i--) //inverse loop because we are going to be destroying shit
        {
            Transform enemy = EnemyManager.current.transform.GetChild(i);
            print(Vector3.Distance(transform.position, enemy.transform.position));
            if (enemy&&Vector3.Distance(transform.position, enemy.transform.position)<explosiveRange)
            {
                HealthManager enemyHealth = enemy.gameObject.GetComponent<HealthManager>();
                if(enemyHealth)
                {
                    enemyHealth.ApplyDamage(damage, penetrating);
                }
            }
        }
    }
}
