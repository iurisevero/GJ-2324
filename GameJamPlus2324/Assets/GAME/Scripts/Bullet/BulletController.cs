using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float bulletSpeed = 10f;
    public Vector3 dir;

    private void Update()
    {
        rb.AddForce(dir * bulletSpeed, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

}