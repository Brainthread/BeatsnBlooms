using UnityEngine;
using UnityEngine.UI;

public class PlantUIHandler : MonoBehaviour
{
    [SerializeField] private PlantGrowthHandler plantGrowthHandler;
    private bool growthSelectionActive = false;
    private int selectedSeedButtonIndex = -1;
    private SeedData selectedSeedData = null;
    [SerializeField] private GameObject seedSelectionButton;
    [SerializeField] private SeedData[] availableSeeds;
    [SerializeField] private GameObject buttonHolder;
    private GameObject[] seedSelectionButtons;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seedSelectionButtons = new GameObject[availableSeeds.Length];
        for(int i = 0; i < availableSeeds.Length; i++)
        {
            int index = i;
            SeedData seed = availableSeeds[index];
            GameObject button = Instantiate(seedSelectionButton);
            seedSelectionButtons[i] = button;
            Button btnComponent = button.GetComponent<Button>();
            btnComponent.onClick.AddListener(() => SetSeedSelection(seed, index));
            button.transform.parent = buttonHolder.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.L)) {
            PlantSeed();
        }    
    }

    void ClearSelection ()
    {
        selectedSeedData = null;
        selectedSeedButtonIndex = -1;
        growthSelectionActive = false;
    }

    void PlantSeed ()
    {
        if (selectedSeedData != null)
            plantGrowthHandler.PlantSeed(selectedSeedData.seedPrefab, 0);
        else
            print("NO THAT WON'T WORK!");
    }

    void SetSeedSelection (SeedData seedData, int buttonIndex)
    {
        if(selectedSeedButtonIndex == buttonIndex)
        {
            ClearSelection();
            return;
        }
        selectedSeedData = seedData;
        selectedSeedButtonIndex = buttonIndex;
        growthSelectionActive = true;
    }
}



