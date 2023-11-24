using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnerAreaController : MonoBehaviour
{
    const string PlayerTag = "Player";
    [HideInInspector] public EarthTreeType plantedTree;
    [SerializeField] private ParticleSystem glow;
    public GameObject plantedTreeObj;
    public bool planted;

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
        Debug.Log($"Col tag: {col.tag}");
        if (col.CompareTag(PlayerTag))
        {
            Debug.Log("OnPlayerEnterTreeSpawnerArea");
            PlayerEnterPlantationAreaEvent playerEnterPlantationAreaEvent = 
                Events.PlayerEnterPlantationAreaEvent;
            playerEnterPlantationAreaEvent.treeSpawnerAreaController = this;
            EventManager.Broadcast(playerEnterPlantationAreaEvent);
        }
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("OnPlayerExitTreeSpawnerArea");
        if (col.CompareTag(PlayerTag))
            EventManager.Broadcast(Events.PlayerExitPlantationAreaEvent);
    }
}
