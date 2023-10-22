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
    public WaveUIController waveUIController;
    public Wave[] waves;
    public int totalMonsters;
    public int currentWaveIndex;
    public float toWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        GameObjectPoolController.AddEntry(GrapeSeedKey, GrapeSeedPrefab, 3, 10);
        GameObjectPoolController.AddEntry(BananaSeedKey, BananaSeedPrefab, 3, 10);
        GameObjectPoolController.AddEntry(AvocadoSeedKey, AvocadoSeedPrefab, 3, 10);
        GameObjectPoolController.AddEntry(StrawberrySeedKey, StrawberrySeedPrefab, 3, 10);
        waveUIController.SetInstaStartButton(false);
        waveUIController.UpdateTime(0f);
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
        waveUIController.SetWaveText(currentWaveIndex+1);
        toWaitTime = currentWave.timeBeforeWave;
        waveUIController.UpdateTime(toWaitTime);
        waveUIController.SetInstaStartButton(true);
        while(toWaitTime >= 0)
        {
            toWaitTime -= Time.deltaTime;
            waveUIController.UpdateTime(toWaitTime);
            yield return new WaitForSeconds(0.1f);
        }
        waveUIController.SetInstaStartButton(false);
        totalMonsters = 0;
        foreach(PairEnemyQuantity pairEnemyQuantity in currentWave.pairEnemyQuantity)
        {
            totalMonsters += pairEnemyQuantity.quantity;
            for(int i=0; i < pairEnemyQuantity.quantity; ++i) { 
                spawnerController.SpawnEnemy(pairEnemyQuantity.enemyTreeType);
                yield return new WaitForSeconds(0.5f);
            }
        }
        waveUIController.SetWaveFill(totalMonsters);
    }

    public void InstaStartWave()
    {
        toWaitTime = -1;
    }

    public void EnemyDied()
    {
        totalMonsters--;
        waveUIController.UpdateWaveFill(totalMonsters);
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
