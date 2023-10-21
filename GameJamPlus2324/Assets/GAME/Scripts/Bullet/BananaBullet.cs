using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBullet : BulletController
{
    float currentSpeed;
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    public override void Update()
    {
        rb.AddForce(transform.forward * currentSpeed, ForceMode.Impulse);
    }

    public override void OnEnable()
    {
        rb.velocity = Vector3.zero;
        currentSpeed = bulletSpeed;
        Invoke("WentBack", timeToBulletDisappear / 2);
        Invoke("Enqueue", timeToBulletDisappear);
    }

    public void WentBack()
    {
        rb.velocity = Vector3.zero;
        currentSpeed *= -1;
    }
}
