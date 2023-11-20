using UnityEngine;

public class PlantationController : MonoBehaviour
{
    [SerializeField] private PlantationUIController plantationUIController;
    [HideInInspector] public EarthTreeType plantedTree;
    public PairEarthTreeTypePrefab[] earthTreePrefabs;
    public bool planted;
    TreeSpawnerAreaController currentTreeSpawnerAreaController;

    private void OnPlayerEnterPlantationAreaEvent(
        PlayerEnterPlantationAreaEvent playerEnterPlantationAreaEvent) => None();
    private void OnPlayerExitPlantationAreaEvent(
        PlayerExitPlantationAreaEvent playerExitPlantationAreaEvent) => None();

    public void None()
    {

    }

    public void Awake()
    {
        EventManager.AddListener<PlayerEnterPlantationAreaEvent>(OnPlayerEnterPlantationAreaEvent);
        EventManager.AddListener<PlayerExitPlantationAreaEvent>(OnPlayerExitPlantationAreaEvent);
    }

    public void Plant(EarthTreeType earthTreeType)
    {
        int ret = currentTreeSpawnerAreaController.Plant(
            earthTreeType,
            GetEarthTreePrefab(earthTreeType),
            GetEarthTreeColor(earthTreeType)
        );

        if(ret == 0)
        {
            plantationUIController.HidePlantButtons();
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
        EventManager.RemoveListener<PlayerEnterPlantationAreaEvent>(OnPlayerEnterPlantationAreaEvent);
        EventManager.RemoveListener<PlayerExitPlantationAreaEvent>(OnPlayerExitPlantationAreaEvent);
    }
}