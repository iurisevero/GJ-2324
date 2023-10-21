using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : Singleton<Player>
{
    const string PlantationAreaTag = "PlantationArea";
    Dictionary<EarthTreeType, int> seeds;
    bool onPlantationArea = false;
    PlantationController currentPlantationArea;

    public Action<EarthTreeType> onPlayerEnteredPlantationArea;
    public Action<EarthTreeType> onPlayerExitedPlantationArea;

    // Start is called before the first frame update
    void Start()
    {
        onPlantationArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onPlantationArea)
        {
            EarthTreeType randonEarthTreeType;
            switch (Random.Range(0, 3))
            {
                case 0:
                    randonEarthTreeType = EarthTreeType.Avocado;
                    break;
                case 1:
                    randonEarthTreeType = EarthTreeType.Banana;
                    break;
                case 2:
                    randonEarthTreeType = EarthTreeType.Grape;
                    break;
                default:
                    randonEarthTreeType = EarthTreeType.Strawberry;
                    break;
            }

            currentPlantationArea.Plant(randonEarthTreeType);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(PlantationAreaTag))
        {
            onPlantationArea = true;
            currentPlantationArea = col.gameObject.GetComponent<PlantationController>();
            onPlayerEnteredPlantationArea?.Invoke(currentPlantationArea.GetTreeType());
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(PlantationAreaTag))
        {
            onPlantationArea = true;
            currentPlantationArea = col.gameObject.GetComponent<PlantationController>();
            onPlayerExitedPlantationArea?.Invoke(currentPlantationArea.GetTreeType());
        }
    }
}