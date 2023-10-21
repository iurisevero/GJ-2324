using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    const string PlantationAreaTag = "PlantationArea";
    Dictionary<EarthTreeType, int> seeds;
    bool onPlantationArea = false;
    PlantationController currentPlantationArea;

    // Start is called before the first frame update
    void Start()
    {
        onPlantationArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && onPlantationArea) {
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
                    randonEarthTreeType = EarthTreeType.Straweberry;
                    break;
            }
            currentPlantationArea.Plant(randonEarthTreeType);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag(PlantationAreaTag)) {
            onPlantationArea = true;
            currentPlantationArea = col.gameObject.GetComponent<PlantationController>();
        }
    }
}
