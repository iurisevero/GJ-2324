using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeBullet : BulletController
{
    [SerializeField] Rigidbody bullet1Rigidbody;
    [SerializeField] Rigidbody bullet2Rigidbody;
    [SerializeField] Rigidbody bullet3Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        bullet1Rigidbody.transform.rotation = Quaternion.Euler(0f, 30f, 0f);
        bullet1Rigidbody.AddRelativeForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        bullet2Rigidbody.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        bullet2Rigidbody.AddRelativeForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        bullet3Rigidbody.transform.rotation = Quaternion.Euler(0f, -30f, 0f);
        bullet3Rigidbody.AddRelativeForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    public override void OnEnable()
    {
        bullet1Rigidbody.velocity = Vector3.zero;
        bullet2Rigidbody.velocity = Vector3.zero;
        bullet3Rigidbody.velocity = Vector3.zero;
        bullet1Rigidbody.transform.position = transform.position;
        bullet2Rigidbody.transform.position = transform.position;
        bullet3Rigidbody.transform.position = transform.position;
        Invoke("Enqueue", timeToBulletDisappear);
    }

    // public override void Enqueue()
    // {
    //     base.Enqueue();
    // }
}
