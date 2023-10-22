using UnityEngine;

public class GrapeEnemy : EnemyController
{
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
            Debug.Log(target);
            navMeshAgent.SetDestination(target);
            return;
        }

        navMeshAgent.SetDestination(destination);
    }
}