using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsSpawner : MonoBehaviour
{
    const string GrapeSeedKey = "GrapeSeed";
    const string BananaSeedKey = "BananaSeed";
    const string AvocadoSeedKey = "AvocadoSeed";
    const string StrawberrySeedKey = "StrawberrySeed";
    public GameObject GrapeSeedPrefab;
    public GameObject BananaSeedPrefab;
    public GameObject AvocadoSeedPrefab;
    public GameObject StrawberrySeedPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObjectPoolController.AddEntry(GrapeSeedKey, GrapeSeedPrefab, 3, 10);
        GameObjectPoolController.AddEntry(BananaSeedKey, BananaSeedPrefab, 3, 10);
        GameObjectPoolController.AddEntry(AvocadoSeedKey, AvocadoSeedPrefab, 3, 10);
        GameObjectPoolController.AddEntry(StrawberrySeedKey, StrawberrySeedPrefab, 3, 10);
        Invoke("CreateSeed", 5f);
        Invoke("CreateSeed", 10f);
        Invoke("CreateSeed", 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSeed() 
    {
        Poolable p = GameObjectPoolController.Dequeue(GrapeSeedKey);
        SeedController seedController = p.GetComponent<SeedController>();
        seedController.transform.position = transform.position;
        seedController.gameObject.SetActive(true);
        seedController.ThrowSeed();
    }
}
