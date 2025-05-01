using UnityEngine;

public class SequencerExplosion : MonoBehaviour
{
    [SerializeField] private float explosionRange = 180;
    [SerializeField] private float explosionDamage = 20;
    [SerializeField] private LayerMask explosionMask;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VFXManager.current.PlayImpactFrames();
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, Vector3.right, explosionRange);
        if(hits.Length>0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                GameObject hitObject = hits[i].transform.gameObject;
                HealthManager hm = hitObject.GetComponent<HealthManager>();
                if (hm)
                {
                    hm.ApplyDamage(explosionDamage, true);
                }
            }
        }
    }
}
