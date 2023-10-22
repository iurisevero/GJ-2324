using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryEnemy : EnemyController
{
    public float stopTime;

    public bool isDirectToTarget;
    public Vector3 target;

    public override void Start()
    {
        base.Start();
        target = GameObject.FindWithTag("SeedSpawner").transform.position;
    }

    public override void Move()
    {
        if (isDirectToTarget)
        {
            navMeshAgent.SetDestination(target);
            return;
        }
        
        navMeshAgent.SetDestination(destination);
        StartCoroutine(StopWalking());
    }

    private IEnumerator StopWalking()
    {
        while(gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(stopTime);
            navMeshAgent.speed = 0;
            yield return new WaitForSeconds(stopTime);
            navMeshAgent.speed = enemySpeed;
        }
    }
}
