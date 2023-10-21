using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public EarthTreeType earthTreeSeedType;
    private float throwForce = 5f;

    public void ThrowSeed()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(1f, 15f));
        _rigidbody.AddForce(Vector3.up * throwForce, ForceMode.Impulse);
    }
}
