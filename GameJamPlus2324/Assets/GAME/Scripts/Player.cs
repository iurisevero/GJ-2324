using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Singleton<Player>
{
    const string PlantationAreaTag = "PlantationArea";
    const string SeedAreaTag = "SeedArea";

    public PlantationUIController plantationUIController;
    public static bool paused;
    Dictionary<EarthTreeType, int> seeds;
    bool onPlantationArea = false;
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
        plantationUIController.avocadoButton.onClick.AddListener(() => Plant(EarthTreeType.Avocado));
        plantationUIController.bananaButton.onClick.AddListener(() => Plant(EarthTreeType.Banana));
        plantationUIController.grapeButton.onClick.AddListener(() => Plant(EarthTreeType.Grape));
        plantationUIController.strawberryButton.onClick.AddListener(() => Plant(EarthTreeType.Strawberry));
        onPlantationArea = false;
        // seeds = new Dictionary<EarthTreeType, int>()
        // {
        //     { EarthTreeType.Avocado, 2 },
        //     { EarthTreeType.Banana, 1 },
        //     { EarthTreeType.Grape, 1 },
        //     { EarthTreeType.Strawberry, 3 },
        // };
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) return;

        if (Input.GetKeyDown(KeyCode.E) && onPlantationArea)
        {
            plantationUIController.Populate(seeds);
            plantationUIController.ShowPlantButtons();
            plantationUIController.HidePressE();
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.E) && onSeedArea)
        {
            seeds[currentSeedArea.earthTreeSeedType]++;
            GameObjectPoolController.Enqueue(currentSeedArea.GetComponent<Poolable>());
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(PlantationAreaTag))
        {
            onPlantationArea = true;
            currentPlantationArea = col.gameObject.GetComponent<PlantationController>();
            
            if(!currentPlantationArea.planted) {
                plantationUIController.ShowPressE();
            }
            
            Debug.Log($"isPlanted: {currentPlantationArea.planted}; Refilling: {refilling}");
            if (
                currentPlantationArea.planted && !refilling && 
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
            currentSeedArea = col.gameObject.GetComponent<SeedController>();
            Debug.Log($"isSeed: {currentSeedArea.earthTreeSeedType};");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(PlantationAreaTag))
        {
            onPlantationArea = false;
            currentPlantationArea = null;
            plantationUIController.HidePressE();
            plantationUIController.HidePlantButtons();
            UnPause();
            if(refilling)
                ResetCurrentFullfillTimer();
        }

        if (col.CompareTag(SeedAreaTag))
        {
            onSeedArea = false;
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

    public void Plant(EarthTreeType earthTreeType)
    {
        int ret = currentPlantationArea.Plant(earthTreeType);
        if (ret == 0)
        {
            seeds[earthTreeType] -= 1;
            plantationUIController.HidePlantButtons();
            UnPause();
        }
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