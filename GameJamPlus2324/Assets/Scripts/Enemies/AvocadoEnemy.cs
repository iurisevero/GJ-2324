using UnityEngine;

public class AvocadoEnemy : EnemyController
{
    public float threshold = 1;
    public float circleRangeMaxDistance = 20f;
    public float moveDistance = 7f;
    private float circleCurrentRange;

    public bool isDirectToTarget;
    public Vector3 target;

    public override void Start()
    {
        base.Start();
        target = GameObject.FindWithTag("SeedSpawner").transform.position;
    }

    public override void Update()
    {
        base.Update();
        Waypoints();
    }

    public override void OnEnable()
    {
        circleCurrentRange = circleRangeMaxDistance;
        base.OnEnable();
    }

    private void Waypoints()
    {
        if (isDirectToTarget)
        {
            navMeshAgent.SetDestination(target);
            return;
        }

        if (Vector3.Distance(navMeshAgent.transform.position, destination) < threshold)
        {
            Move();
        }

        navMeshAgent.SetDestination(destination);
    }

    public override void Move()
    {
        // Debug.Log($"Avocato enemy circleCurrentRange: {circleCurrentRange}");
        destination = Random.insideUnitSphere * circleCurrentRange;
        destination.y = transform.position.y;
        // Debug.Log($"Avocato enemy dest: {destination}");
        circleCurrentRange -= moveDistance;
        circleCurrentRange = Mathf.Max(0, circleCurrentRange);
    }
}