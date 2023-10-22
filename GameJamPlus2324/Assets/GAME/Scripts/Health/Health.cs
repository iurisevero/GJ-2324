using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Action<int> OnHealthValueChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        OnHealthValueChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!gameObject.tag.Equals("Player"))
        {
            Enqueue();
            SeedsSpawner.Instance.EnemyDied();
        }
        else
        {
            // gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
    }

    private void Enqueue()
    {
        Poolable p = gameObject.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }
}