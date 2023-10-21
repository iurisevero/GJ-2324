using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GrapeEnemy : EnemyController
{
    // Start is called before the first frame update
    // public override void Start()
    // {
    //     base.Start();
    // }

    // // Update is called once per frame
    // public override void Update()
    // {

    // }

    // public override void OnEnable()
    // {

    // }

    public override void Move()
    {
        navMeshAgent.SetDestination(destination);
    }
}
