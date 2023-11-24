using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    const string PlantationAreaTag = "PlantationArea";
    const string SeedAreaTag = "Seed";

    public WorldUIController plantationUIController;
    public SeedsCountUIController seedsCountUIController;
    public static bool paused;
    public Dictionary<EarthTreeType, int> seeds { private set; get; }
    bool onSeedArea = false;
    PlantationController currentPlantationArea;
    SeedController currentSeedArea;
    private IEnumerator refillCoroutine;
    private bool refilling;
    [SerializeField] ShootManager shootManager;
    
    

    [Header("Fullfill Ammo")] private float timeToFullfill = 2f;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        refilling = false;
        seeds = new Dictionary<EarthTreeType, int>()
        {
            { EarthTreeType.Avocado, 0 },
            { EarthTreeType.Banana, 0 },
            { EarthTreeType.Grape, 0 },
            { EarthTreeType.Strawberry, 0 },
        };
        seedsCountUIController.UpdateSeedsCount(seeds);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) return;

        if (Input.GetKeyDown(KeyCode.E) && onSeedArea)
        {
            seeds[currentSeedArea.earthTreeSeedType]++;
            seedsCountUIController.UpdateSeedsCount(seeds);
            plantationUIController.HidePressE();
            GameObjectPoolController.Enqueue(currentSeedArea.GetComponent<Poolable>());
            onSeedArea = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(PlantationAreaTag))
        {          
            if (
                !refilling && 
                shootManager._selectedWeaponType.GetWeaponType() == currentPlantationArea.plantedTree
            ) {
                refillCoroutine = RefillAmmo();
                plantationUIController.ShowReload(timeToFullfill);
                StartCoroutine(refillCoroutine);
            }
        }

        if (col.CompareTag(SeedAreaTag))
        {
            onSeedArea = true;
            plantationUIController.ShowPressE();
            currentSeedArea = col.gameObject.GetComponent<SeedController>();
            Debug.Log($"isSeed: {currentSeedArea.earthTreeSeedType};");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(PlantationAreaTag))
        {
            currentPlantationArea = null;
            plantationUIController.HidePressE();
            plantationUIController.HidePlantButtons();
            if(refilling)
                ResetCurrentFullfillTimer();
        }

        if (col.CompareTag(SeedAreaTag))
        {
            onSeedArea = false;
            plantationUIController.HidePressE();
            currentSeedArea = null;
        }
    }

    void ResetCurrentFullfillTimer()
    {
        StopCoroutine(refillCoroutine);
        plantationUIController.HideReload();
        refilling = false;
        Debug.Log("Stop refill in middle");
    }

    IEnumerator RefillAmmo()
    {
        Debug.Log("Refilling ammo");
        refilling = true;
        yield return new WaitForSeconds(timeToFullfill);
        Debug.Log($"CurrentPlantationArea: {currentPlantationArea};");
        Debug.Log($"planted Tree: {currentPlantationArea.plantedTree}");
        shootManager.FullfillAmmo(currentPlantationArea.plantedTree);
        refilling = false;
        plantationUIController.HideReload();
        Debug.Log("Refilled");
    }

    public void RemoveSeed(EarthTreeType earthTreeType)
    {
        seeds[earthTreeType] -= 1;
        UpdateInventoryEvent updateInventoryEvent = Events.UpdateInventoryEvent;
        updateInventoryEvent.seeds = seeds;
        EventManager.Broadcast(updateInventoryEvent);
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
    }

    public static void UnPause()
    {
        Time.timeScale = 1f;
        paused = false;
    }
}