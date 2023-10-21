using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryEnemy : EnemyController
{
    public float stopTime;

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
