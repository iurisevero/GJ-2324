using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float bulletSpeed = 10f;

    private void Update()
    {
        rb.AddForce(new Vector3(0, 0, bulletSpeed), ForceMode.Impulse);
    }

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

}