using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float minRadius = 2f;
    [SerializeField] private float maxRadius = 5f;

    [SerializeField] private float spawnTime = 2f;

    void Start()
    {
        // transform.position = Random.insideUnitSphere * 5;
        InvokeRepeating(nameof(SpawnEnemy), 0, spawnTime);
    }

    void SpawnEnemy()
    {
        int randomEnemyType = Random.Range(0, enemies.Count);
        Vector3 targetPos = Random.insideUnitSphere * maxRadius;
        targetPos.y = 0;
        //targetPos.x = Mathf.Max(minRadius, Mathf.Abs(targetPos.x));

        if (targetPos.z < 0)
            targetPos.z = Mathf.Min(maxRadius, Mathf.Abs(targetPos.z)) * -1;
        else
            targetPos.z = Mathf.Min(maxRadius, Mathf.Abs(targetPos.z));

        if (targetPos.x < 0)
            targetPos.x = Mathf.Max(minRadius, Mathf.Abs(targetPos.x)) * -1;
        else
            targetPos.x = Mathf.Max(minRadius, Mathf.Abs(targetPos.x));


        Debug.Log(targetPos);

        Instantiate(enemies[randomEnemyType], targetPos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, maxRadius);
        Gizmos.DrawWireSphere(Vector3.zero, minRadius);
    }
}