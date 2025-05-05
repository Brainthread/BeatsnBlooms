using UnityEngine;

[CreateAssetMenu(fileName = "SeedData", menuName = "BeatsAndBlooms/SeedData", order = 0)]
[System.Serializable]
public class SeedData : ScriptableObject
{
    public GameObject seedPrefab;
    public Texture2D seedUITexture;

}
