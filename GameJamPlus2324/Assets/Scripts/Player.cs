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
    SeedController currentSeedArea;
    [SerializeField] ShootManager shootManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
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
        if (col.CompareTag(SeedAreaTag))
        {
            onSeedArea = false;
            plantationUIController.HidePressE();
            currentSeedArea = null;
        }
    }

    public void RemoveSeed(EarthTreeType earthTreeType)
    {
        seeds[earthTreeType] -= 1;
        UpdateInventoryEvent updateInventoryEvent = Events.UpdateInventoryEvent;
        updateInventoryEvent.seeds = seeds;
        EventManager.Broadcast(updateInventoryEvent);
    }

    public WeaponSO GetCurrentWeapon()
    {
        return shootManager._selectedWeaponType;
    }

    public void Reload(EarthTreeType earthTreeType)
    {
        shootManager.FullfillAmmo(earthTreeType);
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