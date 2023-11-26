using UnityEngine;

public class PlantationController : MonoBehaviour
{
    [SerializeField] private WorldUIController worldUIController;
    [SerializeField] private PairEarthTreeTypePrefab[] earthTreePrefabs;
    private TreeSpawnerAreaController currentTreeSpawnerAreaController;

    private void OnPlayerExitPlantationAreaEvent(PlayerExitPlantationAreaEvent evt) => 
        OnPlayerExitPlantationAreaHandler();
    private void OnGetEKeyDownEvent(GetEKeyDownEvent evt) => GetEKeyDownHandler();
    public void PlantAvocado() => Plant(EarthTreeType.Avocado);
    public void PlantBanana() => Plant(EarthTreeType.Banana);
    public void PlantGrape() => Plant(EarthTreeType.Grape);
    public void PlantStrawberry() => Plant(EarthTreeType.Strawberry);

    public void Awake()
    {
        EventManager.AddListener<PlayerEnterPlantationAreaEvent>(OnPlayerEnterPlantationAreaHandler);
        EventManager.AddListener<PlayerExitPlantationAreaEvent>(OnPlayerExitPlantationAreaEvent);
        EventManager.AddListener<GetEKeyDownEvent>(OnGetEKeyDownEvent);
    }

    private void Plant(EarthTreeType earthTreeType)
    {
        if(Player.Instance.seeds[earthTreeType] <= 0)
            return;

        int ret = currentTreeSpawnerAreaController.Plant(
            earthTreeType,
            GetEarthTreePrefab(earthTreeType),
            GetEarthTreeColor(earthTreeType)
        );

        if(ret == 0)
        {
            worldUIController.HidePlantButtons();
            Player.Instance.RemoveSeed(earthTreeType);
        }
    }

    private void OnPlayerEnterPlantationAreaHandler(PlayerEnterPlantationAreaEvent evt)
    {
        currentTreeSpawnerAreaController = evt.treeSpawnerAreaController;
        if(!currentTreeSpawnerAreaController.planted)
            worldUIController.ShowPressE();
    }

    private void OnPlayerExitPlantationAreaHandler()
    {
        currentTreeSpawnerAreaController = null;
        worldUIController.HidePressE();
        worldUIController.HidePlantButtons();
    }

    private void GetEKeyDownHandler()
    {
        if(currentTreeSpawnerAreaController != null && !currentTreeSpawnerAreaController.planted)
        {
            worldUIController.Populate(Player.Instance.seeds);
            worldUIController.ShowPlantButtons();
            worldUIController.HidePressE();
        }
    }

    private GameObject GetEarthTreePrefab(EarthTreeType earthTreeType)
    {
        foreach (var earthTreePair in earthTreePrefabs)
        {
            if (earthTreePair.earthTreeType == earthTreeType)
                return earthTreePair.earthTreePrefab;
        }

        return earthTreePrefabs[0].earthTreePrefab;
    }

    private Color GetEarthTreeColor(EarthTreeType earthTreeType)
    {
        foreach (var earthTreePair in earthTreePrefabs)
        {
            if (earthTreePair.earthTreeType == earthTreeType)
                return earthTreePair.earthTreeColor;
        }

        return earthTreePrefabs[0].earthTreeColor;
    }
    
    public void OnDestroy()
    {
        EventManager.RemoveListener<PlayerEnterPlantationAreaEvent>(OnPlayerEnterPlantationAreaHandler);
        EventManager.RemoveListener<PlayerExitPlantationAreaEvent>(OnPlayerExitPlantationAreaEvent);
        EventManager.RemoveListener<GetEKeyDownEvent>(OnGetEKeyDownEvent);
    }
}