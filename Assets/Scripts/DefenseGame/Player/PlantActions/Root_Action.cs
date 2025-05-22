using UnityEngine;

public class Root_Action : PlantAction
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float lineFadeRate = 0.9f;
    [SerializeField] private GameObject rootObject = null;
    [SerializeField] private GameObject rootVFX = null;
    public override void Activate(Plant plant)
    {
        RaycastHit[] hits;
        GameObject rep = Instantiate(rootVFX, transform.position, transform.rotation);
        Vector3 pos = rep.transform.position;
        pos.y = -2.61f;
        rep.transform.position = pos;

        hits = Physics.RaycastAll(plant.transform.position, plant.transform.forward, 200, layerMask);
        if(hits.Length > 0 )
        {
            for(int i = 0; i< hits.Length; i++)
            {
                print("ROOTED");
                GameObject root = Instantiate(rootObject, 
                hits[i].collider.gameObject.transform.position,
                Quaternion.identity);
                root.GetComponent<RootEntity>().SetHost(hits[i].collider.gameObject); 
            }
        }
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
