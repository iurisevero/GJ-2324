using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeBulletAux : MonoBehaviour
{
    [SerializeField] private GrapeBullet grapeBullet;

    private void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.GetComponent<Health>();
        if (otherHealth is null) return;
        var enemyController = other.GetComponent<EnemyController>();
        if(enemyController != null)
        {
            if(enemyController.enemyType == grapeBullet.bulletType)
                otherHealth.TakeDamage(grapeBullet.bulletDamage * 2);
        } else
            otherHealth.TakeDamage(grapeBullet.bulletDamage);
    }
}