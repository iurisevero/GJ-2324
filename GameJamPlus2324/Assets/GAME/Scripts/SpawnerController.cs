using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float minRadius = 2f;
    [SerializeField] private float maxRadius = 5f;

    const string GrapeEnemyPoolKey = "GrapeEnemy";
    const string AvocadoEnemyPoolKey = "AvocadoEnemy";
    const string StrawberryEnemyPoolKey = "StrawberryEnemy";
    const string BananaEnemyPoolKey = "BananaEnemy";

    void Start()
    {
        GameObjectPoolController.AddEntry(GrapeEnemyPoolKey, enemies[0], 3, 15);
        GameObjectPoolController.AddEntry(AvocadoEnemyPoolKey, enemies[1], 3, 15);
        GameObjectPoolController.AddEntry(StrawberryEnemyPoolKey, enemies[2], 3, 15);
        GameObjectPoolController.AddEntry(BananaEnemyPoolKey, enemies[3], 3, 15);
    }

    public void SpawnEnemy(EarthTreeType earthTreeType)
    {
        // Debug.Log($"Spawning enemy: {earthTreeType}");
        Vector3 targetPos = Random.insideUnitSphere * maxRadius;
        targetPos.y = 0;

        if (targetPos.z < 0)
            targetPos.z = Mathf.Min(maxRadius, Mathf.Abs(targetPos.z)) * -1;
        else
            targetPos.z = Mathf.Min(maxRadius, Mathf.Abs(targetPos.z));

        if (targetPos.x < 0)
            targetPos.x = Mathf.Max(minRadius, Mathf.Abs(targetPos.x)) * -1;
        else
            targetPos.x = Mathf.Max(minRadius, Mathf.Abs(targetPos.x));

        string poolKey;
        switch(earthTreeType)
        {
            case EarthTreeType.Grape:
                poolKey = GrapeEnemyPoolKey;
                break;
            case EarthTreeType.Avocado:
                poolKey = AvocadoEnemyPoolKey;
                break;
            case EarthTreeType.Strawberry:
                poolKey = StrawberryEnemyPoolKey;
                break;
            default:
                poolKey = BananaEnemyPoolKey;
                break;
        }

        Poolable p = GameObjectPoolController.Dequeue(poolKey);
        p.gameObject.transform.position = targetPos;
        p.gameObject.transform.rotation = quaternion.identity;
        p.gameObject.SetActive(true);
        // Debug.Log($"Spawned {p.gameObject} at pos: {targetPos}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, maxRadius);
        Gizmos.DrawWireSphere(Vector3.zero, minRadius);
    }
}