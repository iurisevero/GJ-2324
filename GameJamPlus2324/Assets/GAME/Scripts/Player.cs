using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Singleton<Player>
{
    const string PlantationAreaTag = "PlantationArea";

    public PlantationUIController plantationUIController;
    public static bool paused;
    Dictionary<EarthTreeType, int> seeds;
    bool onPlantationArea = false;
    PlantationController currentPlantationArea;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        plantationUIController.avocadoButton.onClick.AddListener(() => Plant(EarthTreeType.Avocado));
        plantationUIController.bananaButton.onClick.AddListener(() => Plant(EarthTreeType.Banana));
        plantationUIController.grapeButton.onClick.AddListener(() => Plant(EarthTreeType.Grape));
        plantationUIController.strawberryButton.onClick.AddListener(() => Plant(EarthTreeType.Strawberry));
        onPlantationArea = false;
        seeds = new Dictionary<EarthTreeType, int>()
        {
            {EarthTreeType.Avocado, 2},
            {EarthTreeType.Banana, 1},
            {EarthTreeType.Grape, 1},
            {EarthTreeType.Strawberry, 3},
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(paused) return;

        if(Input.GetKeyDown(KeyCode.E) && onPlantationArea) {
            plantationUIController.Populate(seeds);
            plantationUIController.Show();
            Pause();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag(PlantationAreaTag)) {
            onPlantationArea = true;
            currentPlantationArea = col.gameObject.GetComponent<PlantationController>();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag(PlantationAreaTag)) {
            onPlantationArea = false;
            currentPlantationArea = null;
            plantationUIController.Hide();
            UnPause();
        }
    }

    public void Plant(EarthTreeType earthTreeType)
    {
        int ret = currentPlantationArea.Plant(earthTreeType);
        if(ret == 0) {
            seeds[earthTreeType] -= 1;
            plantationUIController.Hide();
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
