using UnityEngine;

public abstract class BulletController : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float bulletSpeed = 10f;
    [SerializeField] protected float timeToBulletDisappear = 1.25f;
    public int bulletDamage = 10;

    public virtual void Update()
    {
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    public virtual void OnEnable()
    {
        rb.velocity = Vector3.zero;
        Invoke("Enqueue", timeToBulletDisappear);
    }

    public virtual void Enqueue()
    {
        Poolable p = gameObject.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.GetComponent<Health>();
        if (otherHealth is null) return;
        otherHealth.TakeDamage(bulletDamage);
    }
}