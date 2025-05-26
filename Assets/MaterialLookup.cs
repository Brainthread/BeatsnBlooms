using UnityEngine;

public class MaterialTextureLookup : MonoBehaviour
{
    public static MaterialTextureLookup instance;

    [SerializeField] private Material nullMaterial;
    [SerializeField] private Material rayMaterial;
    [SerializeField] private Material rootMaterial;
    [SerializeField] private Material nukeMaterial;

    [SerializeField] private Sprite nullTex;
    [SerializeField] private Sprite rayTex;
    [SerializeField] private Sprite rootTex;
    [SerializeField] private Sprite nukeTex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Material GetMaterialFromActionType(TileAction.TileActionTypes tileAction)
    {
        Material mat = nullMaterial;
        switch (tileAction)
        {
            case TileAction.TileActionTypes.ROOT:
                mat = rootMaterial;
                break;
            case TileAction.TileActionTypes.ATTACK:
                mat = rayMaterial;
                break;
            case TileAction.TileActionTypes.EXPLOSIVE:
                mat = nukeMaterial;
                break;
        }
        return mat;
    }

    public Sprite GetTextureFromActionType(TileAction.TileActionTypes tileAction)
    {
        Sprite tex = nullTex;
        switch (tileAction)
        {
            case TileAction.TileActionTypes.ROOT:
                tex = rootTex;
                break;
            case TileAction.TileActionTypes.ATTACK:
                tex = rayTex;
                break;
            case TileAction.TileActionTypes.EXPLOSIVE:
                tex = nukeTex;
                break;
        }
        return tex;
    }


}
