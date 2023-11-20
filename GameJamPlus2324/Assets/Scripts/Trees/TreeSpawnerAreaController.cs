using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnerAreaController : MonoBehaviour
{
    const string PlayerTag = "Player";
    [SerializeField] private ParticleSystem glow;
    [SerializeField] private bool planted;
    [HideInInspector] public EarthTreeType plantedTree;
    GameObject plantedTreeObj;

    void Start()
    {
        planted = false;
    }

    public int Plant(EarthTreeType earthTreeType, GameObject earthTreePrefab, Color earthTreeColor)
    {
        if (!planted)
        {
            plantedTree = earthTreeType;
            plantedTreeObj = Instantiate(earthTreePrefab);
            Transform plantedTreeTransform = plantedTreeObj.transform;
            plantedTreeTransform.SetParent(transform);
            // plantedTreeTransform.localScale = Vector3.one;
            plantedTreeTransform.localPosition = new Vector3(0f, 0f, 0f);
            plantedTreeObj.SetActive(true);
            planted = true;
            var glowMain = glow.main;
            glowMain.startSize = 1;
            glowMain.startColor = earthTreeColor;
            glow.Clear();
            glow.Play();
            return 0;
        }
        return -1;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(PlayerTag))
        {
            PlayerEnterPlantationAreaEvent playerEnterPlantationAreaEvent = 
                Events.PlayerEnterPlantationAreaEvent;
            playerEnterPlantationAreaEvent.treeSpawnerAreaController = this;
            playerEnterPlantationAreaEvent.player = col.GetComponent<Player>();
            EventManager.Broadcast(playerEnterPlantationAreaEvent);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(PlayerTag))
        {
            PlayerExitPlantationAreaEvent playerExitPlantationAreaEvent = 
                Events.PlayerExitPlantationAreaEvent;
            EventManager.Broadcast(playerExitPlantationAreaEvent);
        }
    }
}
