using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeBulletAux : MonoBehaviour
{
    [SerializeField] private GrapeBullet grapeBullet;

    private void OnTriggerEnter(Collider other)
    {
        grapeBullet.ChildTriggerEnter(other);
    }
}