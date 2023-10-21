using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantationController : MonoBehaviour
{
    public PairEarthTreeTypePrefab[] earthTreePrefabs;
    public ParticleSystem glow;
    EarthTreeType plantedTree;
    GameObject plantedTreeObj;
    bool planted;

    // Start is called before the first frame update
    void Start()
    {
        planted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Plant(EarthTreeType earthTreeType)
    {
        if(!planted) {    
            plantedTree = earthTreeType;
            plantedTreeObj = Instantiate(GetEarthTreePrefab(earthTreeType));
            Transform plantedTreeTransform = plantedTreeObj.transform;
            plantedTreeTransform.SetParent(transform);
            // plantedTreeTransform.localScale = Vector3.one;
            plantedTreeTransform.localPosition = new Vector3(0f, 2f, 0f);
            plantedTreeObj.SetActive(true);
            planted = true;
            var glowMain = glow.main;
            glowMain.startSize = 1;
            glowMain.startColor = GetEarthTreeColor(earthTreeType);
            glow.Clear();
            glow.Play();
        }
    }

    private GameObject GetEarthTreePrefab(EarthTreeType earthTreeType) 
    {
        foreach (var earthTreePair in earthTreePrefabs)
        {
            if(earthTreePair.earthTreeType == earthTreeType)
                return earthTreePair.earthTreePrefab;   
        }
        return earthTreePrefabs[0].earthTreePrefab;
    }

    private Color GetEarthTreeColor(EarthTreeType earthTreeType) 
    {
        foreach (var earthTreePair in earthTreePrefabs)
        {
            if(earthTreePair.earthTreeType == earthTreeType)
                return earthTreePair.earthTreeColor;   
        }
        return earthTreePrefabs[0].earthTreeColor;
    }
}
