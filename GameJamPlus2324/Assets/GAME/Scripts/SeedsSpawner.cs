using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeedsSpawner : Singleton<SeedsSpawner>
{
    const string GrapeSeedKey = "GrapeSeed";
    const string BananaSeedKey = "BananaSeed";
    const string AvocadoSeedKey = "AvocadoSeed";
    const string StrawberrySeedKey = "StrawberrySeed";
    public GameObject GrapeSeedPrefab;
    public GameObject BananaSeedPrefab;
    public GameObject AvocadoSeedPrefab;
    public GameObject StrawberrySeedPrefab;
    public SpawnerController spawnerController;
    public Wave[] waves;
    public int totalMonsters;
    public int currentWaveIndex;

    // Start is called before the first frame update
    void Start()
    {
        GameObjectPoolController.AddEntry(GrapeSeedKey, GrapeSeedPrefab, 3, 10);
        GameObjectPoolController.AddEntry(BananaSeedKey, BananaSeedPrefab, 3, 10);
        GameObjectPoolController.AddEntry(AvocadoSeedKey, AvocadoSeedPrefab, 3, 10);
        GameObjectPoolController.AddEntry(StrawberrySeedKey, StrawberrySeedPrefab, 3, 10);
        currentWaveIndex = 0;
        totalMonsters = 0;
        StartCurrentWave();
    }

    public void StartCurrentWave()
    {
        StartCoroutine(StartCurrentWaveRoutine());
    }

    public IEnumerator StartCurrentWaveRoutine()
    {
        Wave currentWave = waves[currentWaveIndex];
        yield return new WaitForSeconds(currentWave.timeBeforeWave);

        totalMonsters = 0;
        foreach(PairEnemyQuantity pairEnemyQuantity in currentWave.pairEnemyQuantity)
        {
            totalMonsters += pairEnemyQuantity.quantity;
            for(int i=0; i < pairEnemyQuantity.quantity; ++i) { 
                spawnerController.SpawnEnemy(pairEnemyQuantity.enemyTreeType);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void EnemyDied()
    {
        totalMonsters--;

        if(totalMonsters == 0)
        {
            FinishWave();
        }
    }

    public void FinishWave()
    {
        foreach(PairEnemyQuantity pairSeedQuantity in waves[currentWaveIndex].pairSeedTypeQuantity)
        {
            for(int i=0; i < pairSeedQuantity.quantity; ++i)
                StartCoroutine(CreateSeed(pairSeedQuantity.enemyTreeType, i));
        }

        currentWaveIndex++;
        if(currentWaveIndex >= waves.Length) {
            Debug.Log("Win");
        } else {
            StartCurrentWave();
        }
    }

    public IEnumerator CreateSeed(EarthTreeType earthTreeType, float timeBeforeSpawn) 
    {
        yield return new WaitForSeconds(timeBeforeSpawn);

        string poolKey;
        switch (earthTreeType)
        {
            case EarthTreeType.Grape:
                poolKey = GrapeSeedKey;
                break;
            case EarthTreeType.Strawberry:
                poolKey = StrawberrySeedKey;
                break;
            case EarthTreeType.Avocado:
                poolKey = AvocadoSeedKey;
                break;
            default:
                poolKey = BananaSeedKey;
                break;
        }

        Poolable p = GameObjectPoolController.Dequeue(poolKey);
        SeedController seedController = p.GetComponent<SeedController>();
        seedController.transform.position = transform.position;
        seedController.gameObject.SetActive(true);
        seedController.ThrowSeed();
    }

    [System.Serializable]
    public class Wave 
    {
        public PairEnemyQuantity[] pairEnemyQuantity;
        public PairEnemyQuantity[] pairSeedTypeQuantity;
        public float timeBeforeWave;
    }

    [System.Serializable]
    public class PairEnemyQuantity
    {
        public EarthTreeType enemyTreeType;
        public int quantity;
    }
}
