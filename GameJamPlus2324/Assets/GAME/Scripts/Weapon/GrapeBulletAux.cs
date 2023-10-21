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
        otherHealth.TakeDamage(grapeBullet.bulletDamage);
    }
}