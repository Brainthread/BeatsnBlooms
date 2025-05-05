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
    [SerializeField] private Color selectedButtonColor = Color.green;
    [SerializeField] private Color standardButtonColor = Color.white;
    private GameObject[] seedSelectionButtons;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventHandler.current.onPlayerClickRow += OnPlayerClickRow;
        EventHandler.current.onPlayerUnclickRow += OnPlayerUnclickRow;
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
        Vector2 mouseGridPos = PlayerMouseHandler.current.GetMouseGridPosition();
        if(selectedSeedData != null && !float.IsNaN(mouseGridPos.x))
        {
            mouseGridPos.x = Mathf.Floor(mouseGridPos.x);
            mouseGridPos.y = Mathf.Floor(mouseGridPos.y);
            if(mouseGridPos.y >= 0 && mouseGridPos.y <=3)
            {
                mouseGridPos.x = plantGrowthHandler.PlantPositions[(int)mouseGridPos.y];
                plantGrowthHandler.ShowPlantIndicator(true);
                Vector3 spawnPosition = GridManager.current.GridPositionToWorldPosition(mouseGridPos);
                plantGrowthHandler.SetPlantIndicatorPosition(spawnPosition);
            }
            else
            {
                plantGrowthHandler.ShowPlantIndicator(false);
            }
        }
        else
        {
            plantGrowthHandler.ShowPlantIndicator(false);
        }
    }

    public void OnPlayerClickRow(int row)
    {
        if(row != -1 && selectedSeedData != null)
        {
            plantGrowthHandler.PlantSeed(selectedSeedData.seedPrefab, row);
        }
    }

    public void OnPlayerUnclickRow(int row)
    {
        ClearSelection();
    }

    void ClearSelection ()
    {
        seedSelectionButtons[selectedSeedButtonIndex].GetComponent<Image>().color = standardButtonColor;
        selectedSeedData = null;
        selectedSeedButtonIndex = -1;
        growthSelectionActive = false;
    }


    void SetSeedSelection (SeedData seedData, int buttonIndex)
    {
        if(selectedSeedButtonIndex != -1)
            seedSelectionButtons[selectedSeedButtonIndex].GetComponent<Image>().color = standardButtonColor;
        if(selectedSeedButtonIndex == buttonIndex)
        {
            ClearSelection();
            return;
        }
        selectedSeedData = seedData;
        selectedSeedButtonIndex = buttonIndex;
        growthSelectionActive = true;
        seedSelectionButtons[selectedSeedButtonIndex].GetComponent<Image>().color = selectedButtonColor;
    }
}



