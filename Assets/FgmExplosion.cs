using UnityEngine;

public class FgmExplosion : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float explosiveRange = 40;
    [SerializeField] private bool penetrating = true;
    [SerializeField] private GameObject shockWave;
    [SerializeField] private float shockWaveGrowthSpeed = 5;
    [SerializeField] private float shockWaveFadeSpeed = 5;
    private float distortionAmount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distortionAmount = shockWave.GetComponent<MeshRenderer>().sharedMaterial.GetFloat("_DistortionAmount");
        Material mat = shockWave.GetComponent<MeshRenderer>().sharedMaterial;
        shockWave.GetComponent<MeshRenderer>().sharedMaterial = new Material(mat);
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
    private void Update()
    {
        if (shockWave)
        {
            distortionAmount = Mathf.MoveTowards(distortionAmount, 0, shockWaveFadeSpeed * Time.deltaTime);
            Vector3 shockWaveSize = shockWave.transform.localScale;
            shockWaveSize += Vector3.one * Time.deltaTime * shockWaveGrowthSpeed;
            shockWave.transform.localScale = shockWaveSize;
            shockWave.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_DistortionAmount", distortionAmount);
            if(distortionAmount == 0)
            {
                Destroy(shockWave);
            }
        }
    }
}
