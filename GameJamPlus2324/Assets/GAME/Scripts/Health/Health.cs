using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.LogWarning(gameObject.name + " morreu.");
        if (!gameObject.tag.Equals("Player"))
            Enqueue();
        else
            gameObject.SetActive(false);
    }

    private void Enqueue()
    {
        Poolable p = gameObject.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }
}