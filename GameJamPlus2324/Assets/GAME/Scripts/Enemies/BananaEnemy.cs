using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaEnemy : EnemyController
{
    public float threshold = 1;
    public float zigZagMinDistance = 5f;
    public float zigZagMaxDistance = 20f;
    public float moveDistance = 5f;
    // Start is called before the first frame update
    // public override void Start()
    // {
    //     base.Start();
    // }

    // // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Waypoints();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    private void Waypoints()
    {
        if (Vector3.Distance(navMeshAgent.transform.position, destination) < threshold)
        {
            // Debug.Log($"Waypoints dist current dest: {destination}");
            Move();
            // Debug.Log($"Waypoints dist next dest: {destination}");
        }
        // Debug.Log("Waypoints dist bigger than threshold");
        navMeshAgent.SetDestination(destination);
    }

    private int delta = 1;
    public override void Move()
    {
        Vector3 direction = Vector3.zero - transform.position;
        direction = direction.normalized * moveDistance;
        // Debug.Log($"BananaEnemy direction {direction}");
        direction.x += delta * Random.Range(zigZagMinDistance, zigZagMaxDistance);
        delta *= -1;
        destination = transform.position + direction;
        // Debug.Log($"BananaEnemy destination {destination}");
    }
}
